using System.Net.Http.Headers;
using System.Text;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using EcommerceShop.Contracts.Dtos.UserDtos;
using Newtonsoft.Json;

namespace EcommerceShop.AdminApp.Services
{
    public class UserApiService : IUserApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateUser(UserRegisterDto userRegister)
        {
            var client = _httpClientFactory.CreateClient("myclient");

            var json = JsonConvert.SerializeObject(userRegister);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var url = $"/api/authentication/register";
            var response = await client.PostAsync(url, httpContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<PagedResultDto<UserDto>> GetAllUser(GetUserPagingRequestDto request)    
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient("myclient");
            var url = $"/api/user/getalluser?search={request.search}&pageIndex={request.PageIndex}&pageSize={request.PageSize}";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<PagedResultDto<UserDto>>(data);
            return user;
        }

        public async Task<UserDto> GetUser(Guid userId)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient("myclient");
            var url = $"/api/user/getuser?userid={userId}";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDto>(data);
            return user;
        }
    }
}