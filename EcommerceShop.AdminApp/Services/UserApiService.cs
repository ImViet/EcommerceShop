using System.Net.Http.Headers;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts.Dtos;
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
    }
}