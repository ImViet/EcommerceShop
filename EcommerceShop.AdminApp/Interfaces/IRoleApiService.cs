using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.RoleDtos;

namespace EcommerceShop.AdminApp.Interfaces
{
    public interface IRoleApiService
    {
        Task<ApiResponse<List<RoleDto>>> GetAllRole();
    }
}