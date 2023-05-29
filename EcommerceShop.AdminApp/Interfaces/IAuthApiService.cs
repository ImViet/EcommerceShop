using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.AuthDtos;

namespace EcommerceShop.AdminApp.Interfaces
{
    public interface IAuthApiService
    {
        Task<ApiResponse<string>> LoginAsync(UserLoginDto userLoginDto);
        Task<ApiResponse<bool>> RegisterAsync(UserRegisterDto userRegisterDto);
    }
}
