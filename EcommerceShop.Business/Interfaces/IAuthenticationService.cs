using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<string>> LoginAsync(UserLoginDto userLogin);
        Task<ApiResponse<bool>> RegisterAsync(UserRegisterDto userRegister);
    }
}
