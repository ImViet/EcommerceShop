using AutoMapper;
using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.OrderDtos;
using EcommerceShop.Data.Data;
using EcommerceShop.Data.Entities;
using EcommerceShop.Data.Enums;
namespace EcommerceShop.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly EcommerceShopDbContext _context;
        public OrderService(IMapper mapper, EcommerceShopDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }   
        public async Task<ApiResponse<bool>> CreateOrderAsync(CreateOrderDto newOrder)
        {
            var order = _mapper.Map<Order>(newOrder);
            order.OrderDate = DateTime.Now;
            order.ShipName = "GHTK";
            if(newOrder.PaymentBy != "COD")
            {
                order.Status = OrderStatus.Paying;
            }
            else
            {
                order.Status = OrderStatus.InProgress;
            }
            order.OrderDetails = new List<OrderDetail>();
            foreach (var item in newOrder.Carts)
            {
                order.OrderDetails.Add(new OrderDetail(){
                    ProductId = item.Product.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                });
            }
            _context.Orders.Add(order);
            var result = await _context.SaveChangesAsync();
            if(result > 0)
                return new ApiSuccessResponse<bool>();
            return new ApiErrorResponse<bool>("Lưu đơn hàng thất bại");
        }
    }
}