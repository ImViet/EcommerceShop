using EcommerceShop.Contracts.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Business.Interfaces
{
    public interface IAuthenticateService
    {
        Task<string> LoginAsync(UserLoginDto userLogin);
        Task<bool> RegisterAsync(UserRegisterDto userRegister);
    }
}
