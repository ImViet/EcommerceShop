using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using EcommerceShop.Contracts.Dtos.RoleDtos;
using EcommerceShop.Contracts.Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RoleAdmin")]
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
            if(!data.IsSuccessed)
            {
                return BadRequest("Cannot find any user");
            }
            return Ok(data);
        }
        [HttpGet]
        [Route("GetUser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser([FromQuery]Guid userId)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _userService.GetUserByIdAsync(userId);
            if(!data.IsSuccessed)
            {
                return BadRequest("Cannot find user with id = {userId}");
            }
            return Ok(data);
        }
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody]UserUpdateDto userUpdate)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _userService.UpdateUserAsync(userId, userUpdate);
            if(!data.IsSuccessed)
            {
                return BadRequest();
            }
            return Ok(data);
        }
        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _userService.DeleteUserAsync(userId);
            if(!data.IsSuccessed)
                return BadRequest();
            return Ok(data);
        }
        [HttpPut]
        [Route("RoleAssign")]
        public async Task<IActionResult> RoleAssign(Guid userId, [FromBody]RoleAssignDto roleAssign)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _userService.RoleAssignAsync(userId, roleAssign);
            if(!data.IsSuccessed)
                return BadRequest();
            return Ok(data);
        }
    }
}