using AutoMapper;
using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using EcommerceShop.Contracts.Dtos.ProductImageDtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using EcommerceShop.Contracts.Exceptions;
using EcommerceShop.Data.Data;
using EcommerceShop.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Business.Services
{
    public class ManageProductService : IManageProductService
    {
        private readonly EcommerceShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStorageService _fileStorageService;
        public ManageProductService(EcommerceShopDbContext context, IMapper mapper, IStorageService storageService) 
        {
            _context = context;
            _mapper = mapper;
            _fileStorageService = storageService;
        }
        public Task<List<ProductDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<PagedResultDto<ProductDto>> GetAllPagingAsync(ProductPagingRequestDto request)
        {
            //Join table
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pic in _context.ProductInCategories on p.ProductId equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.CategoryId
                        select new {p, pt, pic};
            //Filter
            if(!string.IsNullOrEmpty(request.search))
            {
                query = query.Where(p => p.pt.Name.Contains(request.search));
            }
            if(request.CategoryIds.Count() > 0) 
            {
                query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            }
            //Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                                    .Take(request.PageSize)
                                    .Select(x => new ProductDto()
                                    {
                                        ProductId = x.p.ProductId,
                                        Price = x.p.Price,
                                        OriginalPrice = x.p.OriginalPrice,
                                        Stock = x.p.Stock,
                                        ViewCount = x.p.ViewCount,
                                        DateCreated = x.p.DateCreated,
                                        Name = x.pt.Name,
                                        Description = x.pt.Description,
                                        Details = x.pt.Details,
                                        SeoAlias = x.pt.SeoAlias,
                                        SeoDescription = x.pt.SeoDescription,
                                        SeoTitle = x.pt.SeoTitle,
                                        LanguageId = x.pt.LanguageId,
                                    }).ToListAsync();
            //Select
            var pagedResult = new PagedResultDto<ProductDto>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data,
            };
            return pagedResult;
        }
        public async Task<ProductDto> GetProductByIdAsync(int productId, string languageId)
        {
            var product = await _context.Products.FindAsync(productId);
            var productTranslation = await _context.ProductTranslations
                                                .FirstOrDefaultAsync(x => x.ProductId == productId && x.LanguageId == languageId);
            var productDto = new ProductDto()
            {
                ProductId = product.ProductId,
                DateCreated = product.DateCreated,
                Description = productTranslation != null ? productTranslation.Description : null,
                LanguageId = productTranslation.LanguageId,
                Details = productTranslation != null ? productTranslation.Details : null,
                Name = productTranslation != null ? productTranslation.Name : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = productTranslation != null ? productTranslation.SeoAlias : null,
                SeoDescription = productTranslation != null ? productTranslation.SeoDescription : null,
                SeoTitle = productTranslation != null ? productTranslation.SeoTitle : null,
                Stock = product.Stock,
                ViewCount = product.ViewCount
            };
            return productDto;

        }
        public async Task<bool> CreateProductAsync(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);
            product.ProductTranslations = new List<ProductTranslation>()
            {
                new ProductTranslation()
                {
                    Name = productCreateDto.Name,
                    Description = productCreateDto.Description,
                    Details = productCreateDto.Details,
                    SeoAlias = productCreateDto.SeoAlias,
                    SeoTitle = productCreateDto.SeoTitle,
                    SeoDescription = productCreateDto.SeoDescription,
                    LanguageId = productCreateDto.LanguageId,
                }
            };

            //Save image
            if(productCreateDto.ThumbnailImage != null) 
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = productCreateDto.ThumbnailImage.Length,
                        ImagePath = await this.SaveFileAsync(productCreateDto.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1,
                    }
                };
            }
            _context.Products.Add(product);
            return await _context.SaveChangesAsync() > 0;

        }
        public async Task<bool> UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            var product = await _context.Products.FindAsync(productUpdateDto.ProductId);
            var productTranslation = await _context.ProductTranslations
                .FirstOrDefaultAsync(x => x.ProductId == productUpdateDto.ProductId && x.LanguageId == productUpdateDto.LanguageId);
            if(product == null || productTranslation == null)
            {
                throw new EcommerceShopException($"Product is not found!!! Product: {productUpdateDto.ProductId}");
            }
            _mapper.Map(productUpdateDto, productTranslation);
            //Save image
            if (productUpdateDto.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.ProductImages
                    .FirstOrDefaultAsync(x => x.IsDefault == true && x.ProductId == productUpdateDto.ProductId);
                if(thumbnailImage != null)
                {
                    thumbnailImage.FileSize = productUpdateDto.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFileAsync(productUpdateDto.ThumbnailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if(product == null)
            {
                throw new EcommerceShopException($"Product is not found!!! Product: {productId}" );
            }
            var images = _context.ProductImages
                    .Where(x => x.IsDefault == true && x.ProductId == productId);
            foreach (var item in images)
            {
                await _fileStorageService.DeleteFileAsync(item.ImagePath);
            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePriceAsync(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new EcommerceShopException($"Product is not found!!! Product: {productId}");
            }
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task AddViewCountAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStockAsync(int productId, int addQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new EcommerceShopException($"Product is not found!!! Product: {productId}");
            }
            product.Stock += addQuantity;
            return await _context.SaveChangesAsync() > 0;
            
        }
        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _fileStorageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<List<ProductImageDto>> GetProductImageAsync(int productId)
        {
            var productImages = await _context.ProductImages.Where(x => x.ProductId == productId).ToListAsync();
            if(productImages == null)
            {
                throw new EcommerceShopException($"Cannot find product image with productId = {productId}");
            }
            var result = _mapper.Map<List<ProductImageDto>>(productImages);
            return result;
        }

        public async Task<bool> AddImageAsync(int productId, ProductImageCreateDto productImageCreateDto)
        {
            if(productImageCreateDto.ImageFile == null)
            {
                throw new EcommerceShopException("No image!!!");
            }
            var productImage = _mapper.Map<ProductImage>(productImageCreateDto);
            productImage.ImagePath = await this.SaveFileAsync(productImageCreateDto.ImageFile);
            productImage.FileSize = productImage.ImagePath.Length; 
            productImage.ProductId = productId;
            productImage.DateCreated = DateTime.Now;
            _context.ProductImages.Add(productImage);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteImageAsync(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if(productImage == null)
            {
                throw new EcommerceShopException($"Cannot find product image with id = {imageId}"); ;
            }
            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateImageAsync(int imageId, ProductImageUpdateDto productImageUpdateDto)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if(productImage == null)
            {
                throw new EcommerceShopException($"Cannot find product image with id = {imageId}");
            }
            if (productImageUpdateDto.ImageFile == null)
            {
                throw new EcommerceShopException("No image!!!");
            }
            _mapper.Map(productImageUpdateDto, productImage);
            productImage.ImagePath = await this.SaveFileAsync(productImageUpdateDto.ImageFile);
            productImage.FileSize = productImage.ImagePath.Length;
            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
