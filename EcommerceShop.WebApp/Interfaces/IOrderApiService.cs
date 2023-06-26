using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.EnumDtos;
using EcommerceShop.Contracts.Dtos.OrderDtos;


namespace EcommerceShop.WebApp.Interfaces
{
    public interface IOrderApiService
    {
        Task<ApiResponse<List<OrderDto>>> GetUserOrder(string userName, string email);
        Task<ApiResponse<bool>> SaveOrder(CreateOrderDto newOrder);
        Task<ApiResponse<bool>> UpdateStatus(Guid orderId, OrderStatusDto status);
    }
}