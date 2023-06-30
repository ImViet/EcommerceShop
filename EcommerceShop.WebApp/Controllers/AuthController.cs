using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EcommerceShop.Contracts.Constants;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using EcommerceShop.WebApp.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

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
        // [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl, UserLoginDto user)
        {
            if(!ModelState.IsValid)
                return View(user);
            var result = await _authService.LoginAsync(user);
            if(!result.IsSuccessed)
            {
                ModelState.AddModelError("", result.Message);
                return View(user);
            }
            var userPrincipal = ValidateToken(result.ResponseObject);
            var authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddHours(1),
                IsPersistent = false
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authProperties);
            if(returnUrl != null && returnUrl !="/Auth/Login")
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            var url = returnUrl;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDto user)
        {
            if(!ModelState.IsValid)
                return View(user);
            var result = await _authService.RegisterAsync(user);
            if(result.IsSuccessed)
            {
                TempData["AlertSuccess"] = "Tạo tài khoản thành công";
                return RedirectToAction("Login", "Auth");
            }
            ModelState.AddModelError("", result.Message);
            return View();
        }
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = JWTSettings.Audience;
            validationParameters.ValidIssuer = JWTSettings.Issuer;
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Key));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }
    }
}