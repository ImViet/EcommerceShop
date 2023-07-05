using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.AdminApp.Controllers
{
    [Authorize(Policy = "RoleAdmin")]
    public class CategoryController: BaseController
    {
        private readonly ICategoryApiService _categoryService;
        public CategoryController(ICategoryApiService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var languageId = HttpContext.Session.GetString("Language");
            var listCate = await _categoryService.GetListCategory(languageId);
            return View(listCate.ResponseObject);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto categoryCreateDto)
        {
            if(!ModelState.IsValid)
            {
                return View(categoryCreateDto);
            }
            categoryCreateDto.LanguageId = HttpContext.Session.GetString("Language");
            var data = await _categoryService.CreateCategory(categoryCreateDto);
            if(!data.IsSuccessed)
            {
                ModelState.AddModelError("", data.Message);
                return View(categoryCreateDto);
            }
            TempData["ModalSuccess"] = "Thêm loại sản phẩm thành công";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int categoryId)
        {
            var languageId = HttpContext.Session.GetString("Language");
            var data = await _categoryService.GetCategoryById(categoryId, languageId);
            var category = new CategoryUpdateDto()
            {
                CategoryId = data.ResponseObject.CategoryId,
                Name = data.ResponseObject.CategoryName,
                SeoAlias = data.ResponseObject.SeoAlias,
                SeoTitle = data.ResponseObject.SeoTitle,
                SeoDescription = data.ResponseObject.SeoDescription
            };
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            if(!ModelState.IsValid)
            {
                return View(categoryUpdateDto);
            }
            categoryUpdateDto.LanguageId = HttpContext.Session.GetString("Language");
            var data = await _categoryService.UpdateCategory(categoryUpdateDto);
            if(!data.IsSuccessed)
            {
                ModelState.AddModelError("", data.Message);
                return View(categoryUpdateDto);
            }
            TempData["ModalSuccess"] = "Cập nhật loại sản phẩm thành công";
            return RedirectToAction("Index", "Category");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int categoryId)
        {
            if(!ModelState.IsValid)
            {
                return View(categoryId);
            }
            var data = await _categoryService.DeleteCategory(categoryId);
            if(!data.IsSuccessed)
            {
                ModelState.AddModelError("", data.Message);
                return View(categoryId);
            }
            TempData["ModalSuccess"] = "Xoá loại sản phẩm thành công";
            return RedirectToAction("Index", "Category");
        }
    }
}