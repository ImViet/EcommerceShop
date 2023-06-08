using EcommerceShop.WebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceShop.WebApp.Views.Shared.Components.LanguageHeader
{
    public class LanguageHeaderViewComponent: ViewComponent
    {
        private readonly ILanguageApiService _languageService;
        public LanguageHeaderViewComponent(ILanguageApiService languageService)
        {
            _languageService = languageService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languageId = HttpContext.Session.GetString("Language"); 
            var result = await _languageService.GetListlanguage();
            ViewData["currentLanguage"] = languageId;
            ViewData["listLanguage"] = result.ResponseObject;
            return View();
        }
    }
}