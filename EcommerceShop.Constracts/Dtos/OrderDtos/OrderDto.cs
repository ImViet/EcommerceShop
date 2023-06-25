using EcommerceShop.Data.Enums;

namespace EcommerceShop.Contracts.Dtos.OrderDtos
{
    public class OrderDto
    {
        public DateTime OrderDate {get; set;}
        public OrderStatusDto Status {get; set;}
        public double Total {get; set;}
        public int Quantity {get; set;}
    }
}