using EcommerceShop.AdminApp.Controllers;
using EcommerceShop.AdminApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.AdminApp.Controllers
{
    [Authorize(Policy = "RoleAdmin")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Language(LanguageViewModel viewModel)
        {
            HttpContext.Session.SetString("Language", viewModel.CurrentLanguageId);

            return Redirect(viewModel.CurrentUrl);
        }
    }
}
