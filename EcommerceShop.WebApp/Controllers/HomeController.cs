using EcommerceShop.Contracts.Constants;
using EcommerceShop.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceShop.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int categoryId)
        {
            HttpContext.Session.SetString("Language", LanguageSetting.DefaultLanguageId);
            HttpContext.Session.SetString("CurrentCategory", categoryId.ToString());
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}