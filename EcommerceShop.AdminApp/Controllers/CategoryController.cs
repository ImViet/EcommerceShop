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
    }
}