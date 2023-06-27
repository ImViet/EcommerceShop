using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.EnumDtos;
using EcommerceShop.Contracts.Dtos.OrderDtos;
using EcommerceShop.Data.Enums;

namespace EcommerceShop.Business.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResponse<List<OrderDto>>> GetAllAsync(OrderStatusDto status);
        Task<ApiResponse<List<OrderDto>>> GetUserOrderAsync(string userName, string email);
        Task<ApiResponse<bool>> CreateOrderAsync(CreateOrderDto order);
        Task<ApiResponse<bool>> UpdateStatusAsync(Guid orderId, OrderStatus status);
    }
}