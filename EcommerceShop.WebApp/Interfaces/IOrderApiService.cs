using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.OrderDtos;

namespace EcommerceShop.WebApp.Interfaces
{
    public interface IOrderApiService
    {
        Task<ApiResponse<bool>> SaveOrder(CreateOrderDto newOrder);
    }
}