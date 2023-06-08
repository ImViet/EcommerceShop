using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.CategoryDtos;
using EcommerceShop.WebApp.Extensions;
using EcommerceShop.WebApp.Interfaces;

namespace EcommerceShop.WebApp.Services
{
    public class CategoryApiService : BaseHttpClientService, ICategoryApiService
    {
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