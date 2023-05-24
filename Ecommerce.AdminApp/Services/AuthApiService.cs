using Ecommerce.AdminApp.Interfaces;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using Newtonsoft.Json;
using System.Text;

namespace Ecommerce.AdminApp.Services
{
    public class AuthApiService : IAuthApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AuthApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> LoginAsync(UserLoginDto userLoginDto)
        {
            var json = JsonConvert.SerializeObject(userLoginDto);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "/api/authentication/login";
            var client = _httpClientFactory.CreateClient("myclient");
            var response = await client.PostAsync(url, httpContent);

            var data = await response.Content.ReadAsStringAsync();
            return data;
        }

        public async Task<bool> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            throw new NotImplementedException();
        }
    }
}
