using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.EnumDtos;
using EcommerceShop.Contracts.Dtos.OrderDtos;

namespace EcommerceShop.AdminApp.Interfaces
{
    public interface IOrderApiService
    {
        Task<ApiResponse<List<OrderDto>>> GetListOrder(OrderStatusDto status);
        Task<ApiResponse<bool>> ChangeOrderStatus(Guid orderId, OrderStatusDto status);
    }
}