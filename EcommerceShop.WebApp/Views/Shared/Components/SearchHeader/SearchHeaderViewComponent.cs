using EcommerceShop.WebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceShop.WebApp.Views.Shared.Components.SearchHeader
{
    public class SearchHeaderViewComponent: ViewComponent
    {
        private readonly ICategoryApiService _categoryService;
        public SearchHeaderViewComponent(ICategoryApiService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languageId = HttpContext.Session.GetString("Language"); 
            var currentCategoryId = HttpContext.Session.GetString("CurrentCategory"); 
            // var result = await _categoryService.GetListCategory(languageId);
            // ViewBag.categories= result.ResponseObject.Select(x => new SelectListItem(){
            //     Text = x.CategoryName,
            //     Value = x.CategoryId.ToString(),
            //     Selected = x.CategoryId == int.Parse(currentCategoryId) 
            // });
            // ViewBag.categories = result.ResponseObject;
            return View();
        }
    }
}