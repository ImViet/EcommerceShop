using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.LanguageDtos;
using EcommerceShop.WebApp.Extensions;
using EcommerceShop.WebApp.Interfaces;

namespace EcommerceShop.WebApp.Services
{
    public class LanguageApiService : BaseHttpClientService, ILanguageApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LanguageApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, httpContextAccessor)
        {

        }
        public async Task<ApiResponse<List<LanguageDto>>> GetListlanguage()
        {
            var url = $"/api/language/getall";
            var result = await GetAsync<ApiResponse<List<LanguageDto>>>(url);
            return result;
        }
    }
}