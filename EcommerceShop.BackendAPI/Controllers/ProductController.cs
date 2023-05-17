using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using EcommerceShop.Contracts.Dtos.ProductImageDtos;
using EcommerceShop.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _productService;
        private readonly IManageProductService _manageProductService;
        public ProductController(IPublicProductService productService, IManageProductService manageProductService)
        {
            _productService = productService;
            _manageProductService = manageProductService;
        }
        //Product
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
        [HttpPatch]
        [Route("UpdatePrice")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var result = await _manageProductService.UpdatePriceAsync(productId, newPrice);
            if(result == false)
            {
                return BadRequest($"Cannot update product with id = {productId}");
            }
            return Ok();
        }
        //Product Image
        [HttpGet]
        [Route("GetAllImage")]
        public async Task<IActionResult> GetProductImage(int productId)
        {
            var result = await _manageProductService.GetProductImageAsync(productId);
            if (result == null)
            {
                return BadRequest("Cannot find image");
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("AddImage")]
        public async Task<IActionResult> AddProductImage(int productId, ProductImageCreateDto productImageCreateDto )
        {
            var result = await _manageProductService.AddImageAsync(productId, productImageCreateDto);
            if(result == false)
            {
                return BadRequest("Cannot add image");
            }
            return Ok();
        }
        [HttpPut]
        [Route("UpdateImage")]
        public async Task<IActionResult> UpdateProductImage(int imageId, ProductImageUpdateDto productImageUpdateDto)
        {
            var result = await _manageProductService.UpdateImageAsync(imageId, productImageUpdateDto);
            if (result == false)
            {
                return BadRequest("Cannot update image");
            }
            return Ok();
        }
        [HttpDelete]
        [Route("DeleteImage")]
        public async Task<IActionResult> DeleteProductImage(int imageId)
        {
            var result = await _manageProductService.DeleteImageAsync(imageId);
            if(result == false)
            {
                return BadRequest("Cannot update image");
            }
            return Ok();
        }
    }
}
