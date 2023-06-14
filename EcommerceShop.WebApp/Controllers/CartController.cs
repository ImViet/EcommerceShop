using EcommerceShop.Contracts.Constants;
using EcommerceShop.Contracts.Dtos.CartDtos;
using EcommerceShop.WebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcommerceShop.WebApp.Controllers
{
    public class CartController: Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductApiService _productService;
        public CartController(ICartService cartService, IProductApiService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }
        [HttpGet]
        public IActionResult GetCart()
        {
            var listCart = _cartService.GetCart();
            return View(listCart);
        }
        [HttpPost]
        public async Task<ViewComponentResult> GetMiniCart()
        {
            return ViewComponent("MiniCart");
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var languageId = HttpContext.Session.GetString("Language");
            var product = await _productService.GetProductById(languageId, productId);
            var cart = GetCartInCookies();
            var cartItem = cart.Find(x => x.Product.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                CartDto newItem = new CartDto()
                {
                    Product = product.ResponseObject,
                    Quantity = 1
                };
                cart.Add(newItem);
            }
            var jsonCart = JsonConvert.SerializeObject(cart);
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                HttpOnly = true,
                IsEssential = true,
            };
            HttpContext.Response.Cookies.Append(CartSetting.CART_COOKIES, jsonCart, cookieOptions);
            return Ok();
        }
        private List<CartDto>? GetCartInCookies()
        {
            var jsonCart = HttpContext.Request.Cookies[CartSetting.CART_COOKIES];
            if (jsonCart == null)
                return new List<CartDto>();
            return JsonConvert.DeserializeObject<List<CartDto>>(jsonCart);
        }
    }
}