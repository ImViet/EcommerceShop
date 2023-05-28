using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using EcommerceShop.Contracts.Dtos.UserDtos;

namespace EcommerceShop.Business.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<PagedResultDto<UserDto>>> GetAllUserAsync(GetUserPagingRequestDto request);
        Task<ApiResponse<UserDto>> GetUserByIdAsync(Guid userId);
        Task<bool> IsUserNameUniqueAsync(string userName);
    }
}