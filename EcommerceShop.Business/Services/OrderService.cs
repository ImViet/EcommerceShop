using AutoMapper;
using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.EnumDtos;
using EcommerceShop.Contracts.Dtos.OrderDtos;
using EcommerceShop.Data.Data;
using EcommerceShop.Data.Entities;
using EcommerceShop.Data.Enums;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ApiResponse<List<OrderDto>>> GetAllAsync(OrderStatusDto status)
        {
            var queryListOrder = await _context.Orders.Where(x => x.Status == (OrderStatus)status).OrderByDescending(x => x.OrderDate).ToListAsync();
            if(queryListOrder == null)
            {
                return new ApiErrorResponse<List<OrderDto>>("Không tìm thấy đơn hàng nào");
            }
            var listOrder = new List<OrderDto>();
            foreach (var order in queryListOrder)
            {
                listOrder.Add(new OrderDto(){
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    Status = ChangeOrderStatusName((OrderStatusDto)order.Status),
                    Email = order.ShipEmail,
                    PhoneNumber = order.ShipPhoneNumber,
                    Quantity = _context.OrderDetails.Where(x => x.OrderId == order.OrderId).Sum(x => x.Quantity),
                    Total = _context.OrderDetails.Where(x => x.OrderId == order.OrderId).Sum(x => x.Quantity * (double)x.Price)
                });
            }
            return new ApiSuccessResponse<List<OrderDto>>(listOrder);
        }   
        public async Task<ApiResponse<List<OrderDto>>> GetUserOrderAsync(string userName, string email)
        {
            var checkAccount = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.Email == email);
            if(checkAccount == null)
            {
                return new ApiErrorResponse<List<OrderDto>>("Không tìm thấy tài khoản");
            }
            var queryListOrder = await _context.Orders.Where(x => x.User.UserName == userName || x.ShipEmail == email).OrderByDescending(x => x.OrderDate).ToListAsync();
            if(queryListOrder == null)
            {
                return new ApiErrorResponse<List<OrderDto>>("Không tìm thấy đơn hàng nào");
            }
            var listOrder = new List<OrderDto>();
            foreach (var order in queryListOrder)
            {
                listOrder.Add(new OrderDto(){
                    OrderDate = order.OrderDate,
                    Status = ChangeOrderStatusName((OrderStatusDto)order.Status),
                    Quantity = _context.OrderDetails.Where(x => x.OrderId == order.OrderId).Sum(x => x.Quantity),
                    Total = _context.OrderDetails.Where(x => x.OrderId == order.OrderId).Sum(x => x.Quantity * (double)x.Price)
                });
            }
            return new ApiSuccessResponse<List<OrderDto>>(listOrder);
        }   
        public async Task<ApiResponse<bool>> CreateOrderAsync(OrderCreateDto newOrder)
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
        public async Task<ApiResponse<bool>> UpdateStatusAsync(Guid orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if(order == null)
                return new ApiErrorResponse<bool>("Không tìm thấy đơn hàng");
            order.Status = status;
            _context.Update(order);
            var result = await _context.SaveChangesAsync();
            if(result > 0)
                return new ApiSuccessResponse<bool>();
            return new ApiErrorResponse<bool>("Lưu đơn hàng thất bại");
        }
        private string ChangeOrderStatusName(OrderStatusDto status)
        {
            string statusName = "";
            switch (status)
            {
                case OrderStatusDto.InProgress:
                    statusName = "Chờ xác nhận";
                    break;
                case OrderStatusDto.Canceled:
                    statusName = "Bị huỷ";
                    break;
                case OrderStatusDto.Confirmed:
                    statusName = "Chờ lấy hàng";
                    break;
                case OrderStatusDto.Error:
                    statusName = "Lỗi";
                    break;
                case OrderStatusDto.Paying:
                    statusName = "Đang thanh toán";
                    break;
                case OrderStatusDto.Shipping:
                    statusName = "Đang giao hàng";
                    break;
                case OrderStatusDto.Success:
                    statusName = "Giao hàng thành công";
                    break;
            }
            return statusName;
        }
    }
}