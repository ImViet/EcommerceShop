using EcommerceShop.Contracts.Dtos.CartDtos;
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
        public async Task<IActionResult> PaymentClient()
        {
            var statusCode = Request.Query["errorCode"];
            if(statusCode == "0")
            {
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