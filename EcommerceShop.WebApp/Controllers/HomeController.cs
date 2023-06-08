using EcommerceShop.Contracts.Constants;
using EcommerceShop.WebApp.Interfaces;
using EcommerceShop.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceShop.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductApiService _productService;

        public HomeController(IProductApiService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int categoryId, string languageId = LanguageSetting.DefaultLanguageId)
        {
            HttpContext.Session.SetString("Language", languageId);
            HttpContext.Session.SetString("CurrentCategory", categoryId.ToString());
            var data = await _productService.GetFeatureProduct(languageId, categoryId = 1, ProductSetting.ProductInHome);
            var productVM = new ProductHomeViewModel()
            {
                Products = data.ResponseObject,
                LastestProduct = null
            };
            return View(productVM);
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