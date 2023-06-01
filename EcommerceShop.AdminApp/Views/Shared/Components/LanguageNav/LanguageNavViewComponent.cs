using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.AdminApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.AdminApp.Views.Shared.Components.LanguageNav
{
    public class LanguageNavViewComponent: ViewComponent
    {
        private readonly ILanguageApiService _languageApiService;
        public LanguageNavViewComponent(ILanguageApiService languageApiService)
        {
            _languageApiService = languageApiService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languages = await _languageApiService.GetAll();
            var languageNav = new LanguageViewModel()
            {
                Languages = languages.ResponseObject,
                CurrentLanguageId = HttpContext?.Session.GetString("Language")
            };
            return View("Default", languageNav);
        }
    }
}