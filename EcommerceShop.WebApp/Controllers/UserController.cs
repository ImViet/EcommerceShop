using EcommerceShop.Contracts.Dtos.EnumDtos;
using EcommerceShop.Contracts.Dtos.OrderDtos;
using EcommerceShop.WebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.WebApp.Controllers
{
    public class UserController: Controller
    {
        private readonly IOrderApiService _orderService;
        public UserController(IOrderApiService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            var userName = User.FindFirst("UserName").Value;
            var email = User.FindFirst("Email").Value;
            var listOrder = await _orderService.GetUserOrder(userName, email);
            ViewData["userOrder"] = listOrder.ResponseObject;
            return View();
        }

    }
}