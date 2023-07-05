using System.Net.Http.Headers;
using System.Text;
using EcommerceShop.AdminApp.Extensions;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.CategoryDtos;
using Newtonsoft.Json;

namespace EcommerceShop.AdminApp.Services
{
    public class CategoryApiService: BaseHttpClientService, ICategoryApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse<List<CategoryDto>>> GetListCategory(string languageId)
        {
            var url = $"/api/category/getall?languageid={languageId}";
            var result = await GetAsync<ApiResponse<List<CategoryDto>>>(url);
            return result;
        }
        public async Task<ApiResponse<CategoryDto>> GetCategoryById(int categoryId, string languageId)
        {
            var url = $"/api/category/getbyid?categoryid={categoryId}&languageid={languageId}";
            var result = await GetAsync<ApiResponse<CategoryDto>>(url);
            return result;
        }

        public async Task<ApiResponse<bool>> CreateCategory(CategoryCreateDto category)
        {
            var client = _httpClientFactory.CreateClient("myclient");
            var json = JsonConvert.SerializeObject(category);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"/api/category/createcategory";
            var response = await client.PostAsync(url, httpContent);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResponse<bool>>(data);
            return JsonConvert.DeserializeObject<ApiErrorResponse<bool>>(data);
        }
        public async Task<ApiResponse<bool>> UpdateCategory(CategoryUpdateDto category)
        {
            var client = _httpClientFactory.CreateClient("myclient");
            var json = JsonConvert.SerializeObject(category);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"/api/category/updatecategory";
            var response = await client.PutAsync(url, httpContent);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResponse<bool>>(data);
            return JsonConvert.DeserializeObject<ApiErrorResponse<bool>>(data);
        }

        public async Task<ApiResponse<bool>> DeleteCategory(int categoryId)
        {
            var client = _httpClientFactory.CreateClient("myclient");
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = $"/api/category/deletecategory?categoryid={categoryId}";
            var response = await client.DeleteAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResponse<bool>>(data);
            return JsonConvert.DeserializeObject<ApiErrorResponse<bool>>(data);
        }


    }
}