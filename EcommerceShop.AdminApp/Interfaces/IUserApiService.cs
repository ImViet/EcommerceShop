using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using EcommerceShop.Contracts.Dtos.UserDtos;

namespace EcommerceShop.AdminApp.Interfaces
{
    public interface IUserApiService
    {
        Task<PagedResultDto<UserDto>> GetAllUser(GetUserPagingRequestDto request);
        Task<bool> CreateUser(UserRegisterDto userRegister);
        Task<UserDto> GetUser(Guid userId);
    }
}