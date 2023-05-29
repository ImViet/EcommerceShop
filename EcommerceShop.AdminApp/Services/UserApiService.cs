using System.Net.Http.Headers;
using System.Text;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts;
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

        public async Task<ApiResponse<bool>> CreateUser(UserRegisterDto userRegister)
        {
            var client = _httpClientFactory.CreateClient("myclient");

            var json = JsonConvert.SerializeObject(userRegister);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var url = $"/api/authentication/register";
            var response = await client.PostAsync(url, httpContent);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResponse<bool>>(data);
            return JsonConvert.DeserializeObject<ApiErrorResponse<bool>>(data);
        }

        public async Task<ApiResponse<PagedResultDto<UserDto>>> GetAllUser(GetUserPagingRequestDto request)    
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient("myclient");
            var url = $"/api/user/getalluser?search={request.search}&pageIndex={request.PageIndex}&pageSize={request.PageSize}";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResponse<PagedResultDto<UserDto>>>(data);
            return JsonConvert.DeserializeObject<ApiErrorResponse<PagedResultDto<UserDto>>>(data);
        }

        public async Task<ApiResponse<UserDto>> GetUser(Guid userId)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient("myclient");
            var url = $"/api/user/getuser?userid={userId}";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResponse<UserDto>>(data);
            return JsonConvert.DeserializeObject<ApiErrorResponse<UserDto>>(data);
        }

        public async Task<ApiResponse<bool>> UpdateUser(Guid userId, UserUpdateDto userUpdate)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient("myclient");
            var url = $"/api/user/updateuser?userid={userId}";
            var json = JsonConvert.SerializeObject(userUpdate);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PutAsync(url, httpContent);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResponse<bool>>(data);
            return JsonConvert.DeserializeObject<ApiErrorResponse<bool>>(data);
        }
    }
}