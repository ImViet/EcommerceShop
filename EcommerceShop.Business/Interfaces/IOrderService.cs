using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.OrderDtos;

namespace EcommerceShop.Business.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResponse<bool>> CreateOrderAsync(CreateOrderDto order);
        // Task<ApiResponse<bool>> UpdateStatus
    }
}