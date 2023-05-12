using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using EcommerceShop.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Business.Services
{
    public class PublicProductService : IPublicProductService
    {
        private readonly EcommerceShopDbContext _context;
        public PublicProductService(EcommerceShopDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResultDto<ProductDto>> GetAllByCategoryIdAsync(int categoryId, int pageIndex, int pageSize)
        {
            //Join table
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pic in _context.ProductInCategories on p.ProductId equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.CategoryId
                        select new { p, pt, pic };
            //Filter
            if (categoryId > 0)
            {
                query = query.Where(p => p.pic.CategoryId == categoryId);
            }
            //Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
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
                TotalRecord = totalRow,
                Items = data,
            };
            return pagedResult;
        }
    }
}
