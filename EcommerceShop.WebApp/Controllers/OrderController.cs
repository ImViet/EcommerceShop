using EcommerceShop.Contracts.Constants;
using EcommerceShop.Contracts.Dtos.CartDtos;
using EcommerceShop.Contracts.Dtos.OrderDtos;
using EcommerceShop.WebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.WebApp.Controllers
{
    public class OrderController: Controller
    {
        private readonly IOrderApiService _orderService;
        private readonly ICartService _cartService;
        public OrderController(IOrderApiService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }
        public async Task<IActionResult> InProcess()
        {
            return View();
        }
        public async Task<IActionResult> SaveOrder(CheckoutDto checkout)
        {
            var newOrder = new OrderCreateDto()
            {
                OrderId = Guid.NewGuid(),
                FirstName = checkout.FirstName,
                LastName = checkout.LastName,
                ShipAddress = checkout.ShipAddress + ", " + checkout.ShipWard + ", " + checkout.ShipDistrict + ", " + checkout.ShipProvince,
                ShipEmail = checkout.ShipEmail,
                ShipPhoneNumber = checkout.ShipPhoneNumber,
                PaymentBy = checkout.PaymentBy,
                Carts = _cartService.GetCart(),
            };
            var result = await _orderService.SaveOrder(newOrder);
            if(checkout.PaymentBy == "Momo")
            {
                return RedirectToAction("PaymentWithMomo", "Momo", newOrder);
            }
            Response.Cookies.Delete(CookiesSetting.CART_COOKIES);
            HttpContext.Session.SetString("CountCart", "0");
            return Redirect("/Order/InProcess");
        }
    }
}