using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts.Dtos.OrderDtos;
using EcommerceShop.Data.Enums;
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
        [HttpPut]
        [Route("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromQuery]Guid orderId, [FromBody]OrderStatus status)
        {
            var result = await _orderService.UpdateStatusAsync(orderId, status);
            if(!result.IsSuccessed)
            {
                return BadRequest("Cập nhật trạng thái thất bại");
            }
            return Ok(result);
        }
    }
}