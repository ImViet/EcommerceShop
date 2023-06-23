using EcommerceShop.Contracts.Dtos.OrderDtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.WebApp.Controllers
{
    public class OrderController: Controller
    {
        public async Task<IActionResult> InProcess()
        {
            return View();
        }
        public async Task<IActionResult> SaveOrder(CheckoutDto checkout)
        {
            return Redirect("/Order/InProcess");
        }
    }
}