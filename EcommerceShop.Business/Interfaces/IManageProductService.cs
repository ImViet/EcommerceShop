using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using EcommerceShop.Contracts.Dtos.ProductImageDtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Business.Interfaces
{
    public interface IManageProductService
    {
        //Product
        Task<List<ProductDto>> GetAllAsync();
        Task<PagedResultDto<ProductDto>> GetAllPagingAsync(ProductPagingRequestDto request);
        Task<ProductDto> GetProductByIdAsync(int productId, string languageId);
        Task<bool> CreateProductAsync(ProductCreateDto productCreateDto);
        Task<bool> UpdateProductAsync(ProductUpdateDto productUpdateDto);
        Task<bool> DeleteProductAsync(int productId);
        Task<bool> UpdatePriceAsync(int productId, decimal newPrice);
        Task AddViewCountAsync(int productId);
        Task<bool> UpdateStockAsync(int productId, int addQuantity);
        //Image
        Task<bool> AddImageAsync(int productId, ProductImageCreateDto productImageCreateDto);
        Task<bool> DeleteImageAsync(int imageId);
        Task<bool> UpdateImageAsync(int imageId, ProductImageUpdateDto productImageUpdateDto);
        Task<List<ProductImageDto>> GetProductImageAsync(int productId);
    }
}
