using EcommerceShop.Contracts.Dtos.CartDtos;
using EcommerceShop.Contracts.Dtos.OrderDtos;
using EcommerceShop.Data.Enums;
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
            if(statusCode == "0")
            {
                var orderId = Request.Query["orderId"];
                _orderService.UpdateStatus(Guid.Parse(orderId), OrderStatusDto.InProgress);
                return RedirectToAction("PaymentSuccess", "Payment");
            }
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