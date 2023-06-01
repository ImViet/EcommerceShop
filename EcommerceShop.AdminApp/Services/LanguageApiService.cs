using System.Net.Http.Headers;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.LanguageDtos;
using Newtonsoft.Json;

namespace EcommerceShop.AdminApp.Services
{
    public class LanguageApiService : ILanguageApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LanguageApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor= httpContextAccessor;
        }
        public async Task<ApiResponse<List<LanguageDto>>> GetAll()
        {
             var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient("myclient");
            var url = $"/api/language/getall";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResponse<List<LanguageDto>>>(data);
            return JsonConvert.DeserializeObject<ApiErrorResponse<List<LanguageDto>>>(data);
        }
    }
}