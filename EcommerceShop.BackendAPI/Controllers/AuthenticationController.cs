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
        public async Task<IActionResult> Login([FromBody]UserLoginDto userLogin)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var result = await _authenticateService.LoginAsync(userLogin);
            if(string.IsNullOrEmpty(result.ResponseObject))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto userRegister)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var result = await _authenticateService.RegisterAsync(userRegister);
            if (result.IsSuccessed == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
