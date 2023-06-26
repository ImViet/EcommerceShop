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
                _orderService.UpdateStatus(Guid.Parse(orderId), OrderStatusDto.InProgress);
                return RedirectToAction("PaymentSuccess", "Payment");
            }
            _orderService.UpdateStatus(Guid.Parse(orderId), OrderStatusDto.Error);
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