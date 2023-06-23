using EcommerceShop.Contracts.Constants;
using EcommerceShop.Contracts.Dtos.CartDtos;
using EcommerceShop.Contracts.Dtos.OrderDtos;
using EcommerceShop.WebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcommerceShop.WebApp.Controllers
{
    public class CheckoutController: Controller
    {
        private readonly ICartService _cartService;
        public CheckoutController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var checkCart = _cartService.CountItem();
            if(checkCart == 0)
                return RedirectToAction("GetCart", "Cart");
            var jsonCart = HttpContext.Request.Cookies[CookiesSetting.CART_COOKIES];
            if(jsonCart == null)
                return View();
            ViewData["Cart"] = JsonConvert.DeserializeObject<List<CartDto>>(jsonCart);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CheckoutDto checkout)
        {
            if(!ModelState.IsValid)
            {
                var jsonCart = HttpContext.Request.Cookies[CookiesSetting.CART_COOKIES];
                ViewData["Cart"] = JsonConvert.DeserializeObject<List<CartDto>>(jsonCart);
                // ViewBag.provinceSelected = checkout.ShipProvince;
                // ViewBag.districtSelected = checkout.ShipDistrict;
                // ViewBag.wardSelected = checkout.ShipWard;
                if(checkout.PaymentBy == null)
                {
                    ModelState.AddModelError("","Vui lòng chọn phương thức thanh toán");
                }
                return View(checkout);
            }
            if(checkout.PaymentBy == "Momo")
            {
                return RedirectToAction("PaymentWithMomo", "Momo", checkout);
            };
            return RedirectToAction("SaveOrder", "Order", checkout);
        }
    }
}