using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using EcommerceShop.Contracts.Dtos.UserDtos;

namespace EcommerceShop.Business.Interfaces
{
    public interface IUserService
    {
        Task<PagedResultDto<UserDto>> GetUserAsync(GetUserPagingRequestDto request);
    }
}