using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _productService;
        private readonly IManageProductService _manageProductService;
        public ProductController(IPublicProductService productService, IManageProductService manageProductService)
        {
            _productService = productService;
            _manageProductService = manageProductService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductByCategory(int categoryId, int pageIndex, int pageSize)
        {
            var products = await _productService.GetAllByCategoryIdAsync(categoryId, pageIndex, pageSize);
            return Ok(products);
        }
        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetProductById(int productId, string languageId)
        {
            var product = await _manageProductService.GetProductByIdAsync(productId, languageId);
            if(product == null)
            {
                return BadRequest($"Cannot find product with id = {productId}");
            }
            return Ok(product);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateProduct([FromForm]ProductCreateDto productCreateDto)
        {
            var result = await _manageProductService.CreateProductAsync(productCreateDto);
            if(result == false)
            {
                return BadRequest("Cannot create new product");
            }
            return Ok();
        }
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateProduct([FromForm]ProductUpdateDto productUpdateDto)
        {
            var result = await _manageProductService.UpdateProductAsync(productUpdateDto);
            if(result == false)
            {
                return BadRequest($"Cannot update product with id = {productUpdateDto.ProductId}");
            }
            return Ok();
        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _manageProductService.DeleteProductAsync(productId);
            if(result == false)
            {
                return BadRequest($"Cannnot delete product with id = {productId}");
            }
            return Ok();
        }
    }
}
