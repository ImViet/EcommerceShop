using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts.Dtos.CategoryDtos;
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
            var data = await _categoryService.GetAllAsync(languageId);
            if(!data.IsSuccessed)
                return BadRequest();
            return Ok(data);
        }
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _categoryService.CreateCategoryAsync(categoryCreateDto);
            if(!data.IsSuccessed)
                return BadRequest();
            return Ok(data);
        }
        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _categoryService.UpdateCategoryAsync(categoryUpdateDto);
            if(!data.IsSuccessed)
                return BadRequest();
            return Ok(data);
        }
    }
}