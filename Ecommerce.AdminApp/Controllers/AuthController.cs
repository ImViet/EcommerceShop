using Ecommerce.AdminApp.Interfaces;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.AdminApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthApiService _authApiService;
        public AuthController(IAuthApiService authApiService)
        {
            _authApiService = authApiService;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var data = await _authApiService.LoginAsync(userLoginDto);
            return View(data);
        }
    }
}
