using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.WebApp.Controllers
{
    public class LanguageController: ControllerBase
    {
        public IActionResult SetLanguage(string languageId)
        {
            HttpContext.Session.SetString("Language", languageId);
            return RedirectToAction("Index");
        }
    }
}