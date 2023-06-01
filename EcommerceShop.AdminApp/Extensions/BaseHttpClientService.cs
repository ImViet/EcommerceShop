using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace EcommerceShop.AdminApp.Extensions
{
    public class BaseHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected BaseHttpClientService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        protected async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient("myclient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TResponse>(data);
            return JsonConvert.DeserializeObject<TResponse>(data);
        }
    }
}