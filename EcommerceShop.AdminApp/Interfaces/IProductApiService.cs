using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;

namespace EcommerceShop.AdminApp.Interfaces
{
    public interface IProductApiService
    {
        Task<ApiResponse<PagedResultDto<ProductDto>>> GetAllProduct(ProductPagingRequestDto request);
    }
}