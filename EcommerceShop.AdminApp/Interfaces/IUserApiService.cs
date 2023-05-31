using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using EcommerceShop.Contracts.Dtos.RoleDtos;
using EcommerceShop.Contracts.Dtos.UserDtos;

namespace EcommerceShop.AdminApp.Interfaces
{
    public interface IUserApiService
    {
        Task<ApiResponse<PagedResultDto<UserDto>>> GetAllUser(GetUserPagingRequestDto request);
        Task<ApiResponse<bool>> CreateUser(UserRegisterDto userRegister);
        Task<ApiResponse<UserDto>> GetUser(Guid userId);
        Task<ApiResponse<bool>> UpdateUser(Guid userId, UserUpdateDto userUpdate);
        Task<ApiResponse<bool>> DeleteUser(Guid userId);
        Task<ApiResponse<bool>> AssignRole(Guid userId, RoleAssignDto roleAssign);
    }
}