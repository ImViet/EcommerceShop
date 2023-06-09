using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts.Dtos.CategoryDtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using EcommerceShop.Contracts.Dtos.ProductImageDtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;
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
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        //ADMIN
        //Product
        [HttpGet]
        [Route("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]ProductPagingRequestDto request)
        {
            var products = await _productService.GetAllPagingAsync(request);
            return Ok(products);
        }
        [HttpGet("GetProductById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductById([FromQuery]int productId, [FromQuery]string languageId)
        {
            var product = await _productService.GetProductByIdAsync(productId, languageId);
            if(product == null)
            {
                return BadRequest($"Cannot find product with id = {productId}");
            }
            return Ok(product);
        }
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm]ProductCreateDto productCreateDto)
        {
            var result = await _productService.CreateProductAsync(productCreateDto);
            if(!result.IsSuccessed)
            {
                return BadRequest("Cannot create new product");
            }
            return Ok(result);
        }
        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm]ProductUpdateDto productUpdateDto)
        {
            var result = await _productService.UpdateProductAsync(productUpdateDto);
            if(!result.IsSuccessed)
            {
                return BadRequest($"Cannot update product with id = {productUpdateDto.ProductId}");
            }
            return Ok(result);
        }
        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productService.DeleteProductAsync(productId);
            if(!result.IsSuccessed)
            {
                return BadRequest($"Cannnot delete product with id = {productId}");
            }
            return Ok(result);
        }
        [HttpPatch]
        [Route("UpdatePrice")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var result = await _productService.UpdatePriceAsync(productId, newPrice);
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
            var result = await _productService.GetProductImageAsync(productId);
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
            var result = await _productService.AddImageAsync(productId, productImageCreateDto);
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
            var result = await _productService.UpdateImageAsync(imageId, productImageUpdateDto);
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
            var result = await _productService.DeleteImageAsync(imageId);
            if(result == false)
            {
                return BadRequest("Cannot update image");
            }
            return Ok();
        }
        [HttpPut]
        [Route("CategoryAssign")]
        public async Task<IActionResult> CategoryAssign(int productId, [FromBody]CategoryAssignDto categoryAssign)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _productService.CategoryAssignAsync(productId, categoryAssign);
            if(!data.IsSuccessed)
                return BadRequest();
            return Ok(data);
        }
        //PUBLIC - for USER
        [HttpGet]
        [Route("GetFeatureProduct")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFeatureProduct(string languageId, int categoryId, int take)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _productService.GetFeatureProductAsync(languageId, categoryId, take);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetLastestProduct")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLastestProduct(string languageId, int take)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _productService.GetLastestProductAsync(languageId, take);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetRelatedProduct")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRelatedProduct(string languageId, int productId, int take)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var data = await _productService.GetRelatedProductAsync(languageId, productId, take);
            return Ok(data);
        }
    }
}
