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
        private readonly ICategoryApiService _categoryService;


        public HomeController(IProductApiService productService, ICategoryApiService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int categoryId, string languageId = LanguageSetting.DefaultLanguageId)
        {
            HttpContext.Session.SetString("Language", languageId);
            HttpContext.Session.SetString("CurrentCategory", categoryId.ToString());
            var products = await _productService.GetFeatureProduct(languageId, categoryId = 0, ProductSetting.ProductInHome);
            var productsLastest = await _productService.GetLastestProduct(languageId, ProductSetting.ProductInHome);
            var categories = await _categoryService.GetListCategory(languageId);
            var productVM = new HomeViewModel()
            {
                Products = products.ResponseObject,
                LastestProduct = productsLastest.ResponseObject,
                Categories = categories.ResponseObject
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