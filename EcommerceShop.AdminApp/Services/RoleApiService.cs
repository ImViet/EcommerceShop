using System.Net.Http.Headers;
using EcommerceShop.AdminApp.Extensions;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.RoleDtos;
using Newtonsoft.Json;

namespace EcommerceShop.AdminApp.Services
{
    public class RoleApiService : BaseHttpClientService, IRoleService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        public RoleApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) 
            : base(httpClientFactory, httpContextAccessor)
        {
        }
        public async Task<ApiResponse<List<RoleDto>>> GetAllRole()
        {
            var url = $"/api/role/getallrole";
            var result = await GetAsync<ApiResponse<List<RoleDto>>>(url);
            return result;
        }
    }
}