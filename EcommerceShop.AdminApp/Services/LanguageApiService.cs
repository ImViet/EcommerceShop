using System.Net.Http.Headers;
using EcommerceShop.AdminApp.Extensions;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.LanguageDtos;
using Newtonsoft.Json;

namespace EcommerceShop.AdminApp.Services
{
    public class LanguageApiService : BaseHttpClientService, ILanguageApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LanguageApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, httpContextAccessor)
        {
        }
        public async Task<ApiResponse<List<LanguageDto>>> GetAll()
        {
            var url = $"/api/language/getall";
            var result = await GetAsync<ApiResponse<List<LanguageDto>>>(url);
            return result;
        }
    }
}