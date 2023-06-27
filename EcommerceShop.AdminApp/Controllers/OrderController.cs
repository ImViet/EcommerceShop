using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts.Dtos.EnumDtos;
using EcommerceShop.Contracts.Dtos.OrderDtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.AdminApp.Controllers
{
    public class OrderController: BaseController
    {
        private readonly IOrderApiService _orderService;
        public OrderController(IOrderApiService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(OrderStatusDto status = OrderStatusDto.InProgress)
        {
            var listOrder = await _orderService.GetListOrder(status);
            switch (status)
            {
                case OrderStatusDto.Confirmed:
                    return View("OrderConfirmed", listOrder.ResponseObject);
                case OrderStatusDto.Shipping:
                    return View("OrderShipping", listOrder.ResponseObject);
                case OrderStatusDto.Success:
                    return View("OrderSuccess", listOrder.ResponseObject);
                case OrderStatusDto.Canceled:
                    return View("OrderCanceled", listOrder.ResponseObject);
                case OrderStatusDto.Error:
                    return View("OrderFail", listOrder.ResponseObject);
                default:
                    return View(listOrder.ResponseObject);
            } 
        }
        [HttpGet]
        public async Task<IActionResult> OrderConfirmed(List<OrderDto> listOrder)
        {
            return View(listOrder);
        }
        [HttpGet]
        public async Task<IActionResult> OrderShipping(List<OrderDto> listOrder)
        {
            return View(listOrder);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeOrderStatus(Guid orderId, OrderStatusDto status, string returnUrl)
        {
            var result = await _orderService.ChangeOrderStatus(orderId, status);
            return Redirect(returnUrl);
        }
    }
}