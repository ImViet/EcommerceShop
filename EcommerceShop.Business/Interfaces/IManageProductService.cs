using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
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
        Task<int> AddImageAsync(int productId, List<IFormFile> files);
        Task<bool> DeleteImageAsync(int productId);
        Task<bool> UpdateImageAsync(int imageId, string caption, bool isDefault);
        Task<List<ProductImageDto>> GetProductImageAsync(int productId);
    }
}
