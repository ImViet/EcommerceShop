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
        public async Task<IActionResult> GetAllProduct()
        {
            var products= await _productService.GetAllByCategoryIdAsync(1, 2, 1);
            return Ok(products);
        }
    }
}
