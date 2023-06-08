using EcommerceShop.WebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.WebApp.Views.Shared.Components.Navbar
{
    public class NavbarViewComponent: ViewComponent
    {
        private readonly ICategoryApiService _categoryService;
        public NavbarViewComponent(ICategoryApiService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languageId = HttpContext.Session.GetString("Language");
            var categories = await _categoryService.GetListCategory(languageId);
            return View("Default", categories.ResponseObject);
        }
    }
}