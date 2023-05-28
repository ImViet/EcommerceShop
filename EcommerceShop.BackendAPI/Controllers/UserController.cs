using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("GetAllUser")]
        public async Task<IActionResult> GetAllUser([FromQuery]GetUserPagingRequestDto request)
        {
            var users = await _userService.GetAllUserAsync(request);
            if(users == null)
            {
                return BadRequest("Cannot find any user");
            }
            return Ok(users);
        }
        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser([FromQuery]Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if(user == null)
            {
                return BadRequest("Cannot find user with id = {userId}");
            }
            return Ok(user);
        }
    }
}