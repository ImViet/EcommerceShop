using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.CategoryDtos;
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
    public interface IProductService
    {
        //Product
        Task<List<ProductDto>> GetAllAsync();
        Task<ApiResponse<PagedResultDto<ProductDto>>> GetAllPagingAsync(ProductPagingRequestDto request);
        Task<PagedResultDto<ProductDto>> GetAllByCategoryIdAsync(int categoryId, int pageIndex, int pageSize);
        Task<ApiResponse<ProductDto>> GetProductByIdAsync(int productId, string languageId);
        Task<ApiResponse<bool>> CreateProductAsync(ProductCreateDto productCreateDto);
        Task<ApiResponse<bool>> UpdateProductAsync(ProductUpdateDto productUpdateDto);
        Task<ApiResponse<bool>> DeleteProductAsync(int productId);
        Task<bool> UpdatePriceAsync(int productId, decimal newPrice);
        Task AddViewCountAsync(int productId);
        Task<bool> UpdateStockAsync(int productId, int addQuantity);
        //Image
        Task<bool> AddImageAsync(int productId, ProductImageCreateDto productImageCreateDto);
        Task<bool> DeleteImageAsync(int imageId);
        Task<bool> UpdateImageAsync(int imageId, ProductImageUpdateDto productImageUpdateDto);
        Task<List<ProductImageDto>> GetProductImageAsync(int productId);
        Task<ApiResponse<bool>> CategoryAssignAsync(int productId, CategoryAssignDto categoryAssign);
    }
}
