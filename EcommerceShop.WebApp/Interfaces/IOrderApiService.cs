using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.OrderDtos;
using EcommerceShop.Data.Enums;

namespace EcommerceShop.WebApp.Interfaces
{
    public interface IOrderApiService
    {
        Task<ApiResponse<bool>> SaveOrder(CreateOrderDto newOrder);
        Task<ApiResponse<bool>> UpdateStatus(Guid orderId, OrderStatusDto status);
    }
}