using EcommerceShop.Contracts.Dtos.CartDtos;
using EcommerceShop.WebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcommerceShop.WebApp.Controllers
{
    public class CartController: Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<IActionResult> GetCart()
        {
            var listCart =_cartService.GetCart(); 
            return View(listCart);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var languageId = HttpContext.Session.GetString("Language");
            _cartService.AddToCart(languageId, productId);
            return Ok();
        }
    }
}