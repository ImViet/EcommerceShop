using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.CategoryDtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;

namespace EcommerceShop.AdminApp.Interfaces
{
    public interface IProductApiService
    {
        Task<ApiResponse<PagedResultDto<ProductDto>>> GetAllProduct(ProductPagingRequestDto request);
        Task<ApiResponse<ProductDto>> GetProductById(int productId, string languageId);
        Task<ApiResponse<bool>> CreateProduct(ProductCreateDto product);
        Task<ApiResponse<bool>> DeleteProduct(int productId);
        Task<ApiResponse<bool>> AssignCategory(int productId, CategoryAssignDto categoryAssign);
    }
}