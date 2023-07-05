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
        [HttpGet]
        [Route("GetById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int categoryId, string languageId)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _categoryService.GetByIdAsync(categoryId, languageId);
            if(!data.IsSuccessed)
                return BadRequest();
            return Ok(data);
        }
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody]CategoryCreateDto categoryCreateDto)
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
        public async Task<IActionResult> UpdateCategory([FromBody]CategoryUpdateDto categoryUpdateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _categoryService.UpdateCategoryAsync(categoryUpdateDto);
            if(!data.IsSuccessed)
                return BadRequest(data.Message);
            return Ok(data);
        }
        [HttpDelete]
        [Route("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _categoryService.DeleteCategoryAsync(categoryId);
            if(!data.IsSuccessed)
                return BadRequest();
            return Ok(data);
        }
    }
}