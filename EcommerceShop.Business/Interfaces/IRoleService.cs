using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.RoleDtos;
namespace EcommerceShop.Business.Interfaces
{
    public interface IRoleService
    {
        Task<ApiResponse<List<RoleDto>>> GetAllRoleAsync();
    }
}