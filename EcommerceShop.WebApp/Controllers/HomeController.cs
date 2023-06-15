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
        private readonly ICartService _cartService;

        public HomeController(IProductApiService productService, ICategoryApiService categoryService, ICartService cartService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index(int categoryId)
        {
            var languageId = HttpContext.Session.GetString("Language");
            if (languageId == null)
            {
                languageId = LanguageSetting.DefaultLanguageId;
                HttpContext.Session.SetString("Language", languageId);
            }
            HttpContext.Session.SetString("CurrentCategory", categoryId.ToString());
            HttpContext.Session.SetString("CountCart", _cartService.CountItem().ToString());
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
        [HttpPost]
        public async Task<ViewComponentResult> GetProductByCate(int categoryId)
        {
            var languageId = HttpContext.Session.GetString("Language");
            var productByCate = await _productService.GetFeatureProduct(languageId, categoryId, ProductSetting.ProductInHome);
            return ViewComponent("ProductHomeByCate", productByCate.ResponseObject);
        }
    }
}