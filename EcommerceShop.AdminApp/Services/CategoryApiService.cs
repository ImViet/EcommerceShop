using EcommerceShop.AdminApp.Extensions;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.CategoryDtos;

namespace EcommerceShop.AdminApp.Services
{
    public class CategoryApiService: BaseHttpClientService, ICategoryApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, httpContextAccessor)
        {
        }
        public async Task<ApiResponse<List<CategoryDto>>> GetListCategory(string languageId)
        {
            var url = $"/api/category/getall?languageid={languageId}";
            var result = await GetAsync<ApiResponse<List<CategoryDto>>>(url);
            return result;
        }
    }
}