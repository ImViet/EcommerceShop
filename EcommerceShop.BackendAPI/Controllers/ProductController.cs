using EcommerceShop.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _productService;
        public ProductController(IPublicProductService productService) 
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct(int categoryId, int pageIndex, int pageSize)
        {
            var products= await _productService.GetAllByCategoryIdAsync(categoryId, pageIndex, pageSize);
            return Ok(products);
        }
    }
}
