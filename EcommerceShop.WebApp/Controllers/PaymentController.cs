using EcommerceShop.Contracts.Constants;
using EcommerceShop.Contracts.Dtos.CartDtos;
using EcommerceShop.Contracts.Dtos.EnumDtos;
using EcommerceShop.Contracts.Dtos.OrderDtos;
using EcommerceShop.WebApp.Extensions;
using EcommerceShop.WebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EcommerceShop.WebApp.Controllers
{
    public class PaymentController: Controller
    {
        private readonly IOrderApiService _orderService;
        public PaymentController(IOrderApiService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> PaymentClient(CheckoutDto checkout)
        {
            var statusCode = Request.Query["errorCode"];
            var orderId = Request.Query["orderId"];
            if(statusCode == "0")
            {
                await _orderService.UpdateStatus(Guid.Parse(orderId), OrderStatusDto.InProgress);
                Response.Cookies.Delete(CookiesSetting.CART_COOKIES);
                HttpContext.Session.SetString("CountCart", "0");
                return RedirectToAction("PaymentSuccess", "Payment");
            }
            await _orderService.UpdateStatus(Guid.Parse(orderId), OrderStatusDto.Error);
            return RedirectToAction("PaymentFail", "Payment");
        }
        public async Task<IActionResult> PaymentSuccess()
        {
            return View();
        }
        public async Task<IActionResult> PaymentFail()
        {
            return View();
        }
    }
}