using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts.Dtos.ProductDtos;
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
        public async Task<IActionResult> Index(string searchKeyword, int pageIndex = 1, int pageSize = 5)
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
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm]ProductCreateDto product)
        {
            if(!ModelState.IsValid)
                return View(product);
            var data = await _productService.CreateProduct(product);
            if(data.IsSuccessed)
            {
                TempData["ModalSuccess"] = "Thêm sản phẩm thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", data.Message);
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int productId)
        {
            var languageId = HttpContext.Session.GetString("Language");
            var data = await _productService.GetProductById(productId, languageId);
            var productUpdate = new ProductUpdateDto()
            {
                Name = data.ResponseObject.Name,
                Details = data.ResponseObject.Details,
                Description = data.ResponseObject.Description,
                SeoAlias = data.ResponseObject.SeoAlias,
                SeoTitle = data.ResponseObject.SeoTitle,
                SeoDescription = data.ResponseObject.SeoDescription,
            };
            return View(productUpdate);
        }
    }
}