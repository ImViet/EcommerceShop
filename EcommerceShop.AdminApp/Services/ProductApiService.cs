using EcommerceShop.AdminApp.Extensions;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;

namespace EcommerceShop.AdminApp.Services
{
    public class ProductApiService : BaseHttpClientService, IProductApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, httpContextAccessor)
        {

        }
        public async Task<ApiResponse<PagedResultDto<ProductDto>>> GetAllProduct(ProductPagingRequestDto request)
        {
            return new ApiResponse<PagedResultDto<ProductDto>>();
        }
    }
}