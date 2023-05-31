using EcommerceShop.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController: ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        [Route("GetAllRole")]
        public async Task<IActionResult> GetAllRole()
        {
            var data = await _roleService.GetAllRoleAsync();
            if(data == null)
                return BadRequest();
            return Ok(data);
        }
    }
}