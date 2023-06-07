using EcommerceShop.WebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceShop.WebApp.Views.Shared.Components.CategoryHeader
{
    public class CategoryHeaderViewComponent: ViewComponent
    {
        private readonly ICategoryApiService _categoryService;
        public CategoryHeaderViewComponent(ICategoryApiService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languageId = HttpContext.Session.GetString("Language"); 
            var currentCategoryId = HttpContext.Session.GetString("CurrentCategory"); 
            var result = await _categoryService.GetListCategory(languageId);
            ViewBag.categories= result.ResponseObject.Select(x => new SelectListItem(){
                Text = x.CategoryName,
                Value = x.CategoryId.ToString(),
                Selected = int.Parse(currentCategoryId) == x.CategoryId
            });
            // ViewBag.categories = result.ResponseObject;
            return View();
        }
    }
}