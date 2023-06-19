using System.Text;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using EcommerceShop.WebApp.Interfaces;
using Newtonsoft.Json;

namespace EcommerceShop.WebApp.Services
{
    public class AuthApiService : IAuthApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AuthApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResponse<string>> LoginAsync(UserLoginDto userLoginDto)
        {
            var client = _httpClientFactory.CreateClient("myclient");
            var json = JsonConvert.SerializeObject(userLoginDto);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "/api/authentication/login";
            var response = await client.PostAsync(url, httpContent);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResponse<string>>(data);
            }
            return JsonConvert.DeserializeObject<ApiErrorResponse<string>>(data);
        }

        public async Task<ApiResponse<bool>> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var client = _httpClientFactory.CreateClient("myclient");
            var json = JsonConvert.SerializeObject(userRegisterDto);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "/api/authentication/register";
            var response = await client.PostAsync(url, httpContent);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResponse<bool>>(data);
            }
            return JsonConvert.DeserializeObject<ApiErrorResponse<bool>>(data);
        }
    }
}