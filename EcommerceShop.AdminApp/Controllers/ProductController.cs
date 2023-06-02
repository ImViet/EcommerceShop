using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.AdminApp.Controllers
{
    public class ProductController: BaseController
    {
        private readonly IProductApiService _productService;
        public ProductController(IProductApiService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchKeyword, int pageIndex = 1, int pageSize = 10)
        {
            var languageId = HttpContext.Session.GetString("Language");
            var request = new ProductPagingRequestDto()
            {
                search =searchKeyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId,
            };
            var data = await _productService.GetAllProduct(request);
            ViewData["searchKeyword"] = request.search;
            return View(data.ResponseObject);
        }
    }
}