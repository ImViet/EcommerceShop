using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts.Dtos.OrderDtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService) 
        {
            _orderService = orderService;
        }
        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderDto newOrder)
        {
            var result = await _orderService.CreateOrderAsync(newOrder);
            if(!result.IsSuccessed)
            {
                return BadRequest("Không thể tạo đơn hàng");
            }
            return Ok(result);
        }
    }
}