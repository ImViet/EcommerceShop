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
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _userService.GetAllUserAsync(request);
            if(data == null)
            {
                return BadRequest("Cannot find any user");
            }
            return Ok(data);
        }
        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser([FromQuery]Guid userId)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _userService.GetUserByIdAsync(userId);
            if(data == null)
            {
                return BadRequest("Cannot find user with id = {userId}");
            }
            return Ok(data);
        }
    }
}