using EcommerceShop.Contracts.Constants;
using EcommerceShop.WebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.WebApp.Views.Shared.Components.RelatedProduct
{
    public class RelatedProductViewComponent: ViewComponent
    {
        private readonly IProductApiService _productService;
        public RelatedProductViewComponent(IProductApiService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            var languageId = HttpContext.Session.GetString("Language");
            var products = await _productService.GetRelatedProduct(languageId, productId, ProductSetting.ProductInHome);
            return View("Default", products.ResponseObject);
        }
    }
}