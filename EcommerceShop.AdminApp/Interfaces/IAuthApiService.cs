using EcommerceShop.Contracts.Dtos.AuthDtos;

namespace Ecommerce.AdminApp.Interfaces
{
    public interface IAuthApiService
    {
        Task<string> LoginAsync(UserLoginDto userLoginDto);
        Task<bool> RegisterAsync(UserRegisterDto userRegisterDto);
    }
}
