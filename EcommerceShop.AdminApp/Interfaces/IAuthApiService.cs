using EcommerceShop.Contracts.Dtos.AuthDtos;

namespace EcommerceShop.AdminApp.Interfaces
{
    public interface IAuthApiService
    {
        Task<string> LoginAsync(UserLoginDto userLoginDto);
        Task<bool> RegisterAsync(UserRegisterDto userRegisterDto);
    }
}
