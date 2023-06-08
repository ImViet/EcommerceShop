using EcommerceShop.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController: ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        [Route("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategory(string languageId)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _categoryService.GetAll(languageId);
            if(!data.IsSuccessed)
                return BadRequest();
            return Ok(data);
        }
    }
}