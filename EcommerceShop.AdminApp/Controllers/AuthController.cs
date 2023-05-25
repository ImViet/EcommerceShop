using System.Security.Claims;
using System.Text;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using EcommerceShop.Contracts.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
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
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var token = await _authApiService.LoginAsync(userLoginDto);
            var userPrincipal = ValidateToken(token);
            var authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(10),
                IsPersistent = false,
            };
            HttpContext.Session.SetString("Token", token);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal,
                authProperties
                );
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login", "Auth");
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
