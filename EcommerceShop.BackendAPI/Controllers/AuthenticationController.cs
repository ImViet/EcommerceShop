using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticateService;
        public AuthenticationController(IAuthenticationService authenticateService)
        {
            _authenticateService = authenticateService;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm]UserLoginDto userLogin)
        {
            var resultToken = await _authenticateService.LoginAsync(userLogin);
            if(string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("Username or Password is incorrect");
            }
            return Ok(new { token = resultToken });
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegister)
        {
            var result = await _authenticateService.RegisterAsync(userRegister);
            if (result == false)
            {
                return BadRequest("Register is unsuccessfull");
            }
            return Ok();
        }
    }
}
