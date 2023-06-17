using EcommerceShop.Contracts.Dtos.AuthDtos;
using EcommerceShop.WebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.WebApp.Controllers
{
    public class AuthController: Controller
    {
        private readonly IAuthApiService _authService;
        public AuthController(IAuthApiService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto user)
        {
            if(!ModelState.IsValid)
                return View(user);
            var result = await _authService.LoginAsync(user);
            if(!result.IsSuccessed)
            {
                ModelState.AddModelError("", result.Message);
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser()
        {
            if(!ModelState.IsValid)
                return View();
            return View();
        }
    }
}