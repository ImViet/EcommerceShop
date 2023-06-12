using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using EcommerceShop.WebApp.Extensions;
using EcommerceShop.WebApp.Interfaces;

namespace EcommerceShop.WebApp.Services
{
    public class ProductApiService: BaseHttpClientService, IProductApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<List<ProductDto>>> GetFeatureProduct(string languageId, int categoryId, int take)
        {
            var url = $"/api/product/getfeatureproduct?languageId={languageId}&categoryId={categoryId}&take={take}";
            var data = await GetAsync<ApiResponse<List<ProductDto>>>(url);
            return data;
        }

        public async Task<ApiResponse<List<ProductDto>>> GetLastestProduct(string languageId, int take)
        {
            var url = $"/api/product/getlastestproduct?languageId={languageId}&take={take}";
            var data = await GetAsync<ApiResponse<List<ProductDto>>>(url);
            return data;
        }
        public async Task<ApiResponse<ProductDto>> GetProductById(string languageId, int productId)
        {
            var url = $"/api/product/getproductbyid?languageId={languageId}&productId={productId}";
            var data = await GetAsync<ApiResponse<ProductDto>>(url);
            return data;
        }
        public async Task<ApiResponse<List<ProductDto>>> GetRelatedProduct(string languageId, int productId, int take)
        {
            var url = $"/api/product/getrelatedproduct?languageId={languageId}&productId={productId}&take={take}";
            var data = await GetAsync<ApiResponse<List<ProductDto>>>(url);
            return data;
        }
        public async Task<ApiResponse<PagedResultDto<ProductDto>>> GetProductByCategory(ProductPagingRequestDto request)
        {
            var url = $"/api/product/getallpaging?languageId={request.LanguageId}&search={request.Search}&categoryId={request.CategoryId}&pageIndex={request.PageIndex}&pageSize={request.PageSize}";
            var data = await GetAsync<ApiResponse<PagedResultDto<ProductDto>>>(url);
            return data;
        }
    }
}