using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.CategoryDtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceShop.AdminApp.Controllers
{
    public class ProductController: BaseController
    {
        private readonly IProductApiService _productService;
        private readonly ICategoryApiService _categoryService;
        public ProductController(IProductApiService productService, ICategoryApiService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchKeyword, int? categoryId, int pageIndex = 1, int pageSize = 5)
        {
            var languageId = HttpContext.Session.GetString("Language");
            var request = new ProductPagingRequestDto()
            {
                Search =searchKeyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId,
                CategoryId = categoryId
            };
            var data = await _productService.GetAllProduct(request);
            ViewData["searchKeyword"] = request.Search;
            var categories = await _categoryService.GetListCategory(languageId);
            ViewBag.Categories = categories.ResponseObject.Select(x => new SelectListItem(){
                Text = x.CategoryName,
                Value = x.CategoryId.ToString(),
                Selected = categoryId.HasValue && categoryId.Value == x.CategoryId
            });
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
        [HttpPost]
        public async Task<ViewComponentResult> GetCateAssigned(int productId)
        {
            var categories = await GetCategoryAssigned(productId);
            return ViewComponent("CategoryAssign", categories);
        }
        [HttpPost]
        public async Task<IActionResult> AssignCategory(CategoryAssignDto cate)
        {
            if(!ModelState.IsValid)
                return View(cate);
            var data = await _productService.AssignCategory(cate.ProductId, cate);
            if(data.IsSuccessed)
            {
                TempData["ModalSuccess"] = "Cập nhật danh mục thành công";
                return RedirectToAction("Index", "Product");
            }
            ModelState.AddModelError("", data.Message);
            return View();
        }
        private async Task<CategoryAssignDto> GetCategoryAssigned(int productId)
        {
            var languageId = HttpContext.Session.GetString("Language");
            var product = await _productService.GetProductById(productId, languageId);
            var categories = await _categoryService.GetListCategory(languageId);
            var categoryAssigned = new CategoryAssignDto();
            foreach (var category in categories.ResponseObject)
            {
                categoryAssigned.Categories.Add(new SelectedDto()
                {
                    Id = category.CategoryId.ToString(),
                    Name = category.CategoryName,
                    Selected = product.ResponseObject.Categories.Contains(category.CategoryName)
                });
            }
            return categoryAssigned;
        }
    }
}