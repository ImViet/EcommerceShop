using EcommerceShop.Contracts.Dtos.EnumDtos;

namespace EcommerceShop.Contracts.Dtos.OrderDtos
{
    public class OrderDto
    {
        public DateTime OrderDate {get; set;}
        public string Status {get; set;}
        public double Total {get; set;}
        public int Quantity {get; set;}
    }
}