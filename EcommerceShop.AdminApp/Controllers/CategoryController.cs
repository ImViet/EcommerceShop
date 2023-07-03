using EcommerceShop.AdminApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.AdminApp.Controllers
{
    // [Authorize(Policy = "RoleAdmin")]
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
    }
}