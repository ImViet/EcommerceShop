using EcommerceShop.Contracts.Constants;
using EcommerceShop.Contracts.Dtos.RequestDtos;
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
        [HttpGet]
        public async Task<IActionResult> GetProductByCategory(int categoryId, int pageIndex = 1, int PageSize = ProductSetting.ProductInCategory)
        {
            var languageId = HttpContext.Session.GetString("Language");
            var request = new ProductPagingRequestDto()
            {
                PageIndex = pageIndex,
                PageSize = PageSize,
                CategoryId = categoryId,
                LanguageId = languageId
            };
            var product = await _productService.GetProductByCategory(request);
            return View(product.ResponseObject);
        }
        [HttpGet]
        public async Task<IActionResult> ProductDetail(int productId)
        {
            var languageId = HttpContext.Session.GetString("Language");
            var product = await _productService.GetProductById(languageId, productId);
            return View(product.ResponseObject);
        }
    }
}