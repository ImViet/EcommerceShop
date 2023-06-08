using EcommerceShop.Contracts.Constants;
using EcommerceShop.WebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.WebApp.Controllers
{
    public class ProductController: Controller
    {
        private readonly IProductApiService _productService;
        public ProductController(IProductApiService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<ViewComponentResult> GetProductByCate(int categoryId)
        {
            var languageId = HttpContext.Session.GetString("Language");
            var productByCate = await _productService.GetFeatureProduct(languageId, categoryId, ProductSetting.ProductInHome);
            return ViewComponent("ProductHomeByCate", productByCate.ResponseObject);
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct(string languageId, int productId)
        {
            var languageId = HttpContext.Session.GetString("Language");
            return View();
        }
    }
}