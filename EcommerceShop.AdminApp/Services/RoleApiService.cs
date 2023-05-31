using System.Net.Http.Headers;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.RoleDtos;
using Newtonsoft.Json;

namespace EcommerceShop.AdminApp.Services
{
    public class RoleApiService : IRoleService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        public RoleApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse<List<RoleDto>>> GetAllRole()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient("myclient");
            var url = $"/api/role/getallrole";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResponse<List<RoleDto>>>(data);
            return JsonConvert.DeserializeObject<ApiErrorResponse<List<RoleDto>>>(data);
        }
    }
}