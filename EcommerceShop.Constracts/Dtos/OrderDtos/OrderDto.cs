using EcommerceShop.Contracts.Dtos.EnumDtos;

namespace EcommerceShop.Contracts.Dtos.OrderDtos
{
    public class OrderDto
    {
        public Guid OrderId {get; set;}
        public int No {get; set;}
        public DateTime OrderDate {get; set;}
        public string Email {get; set;}
        public string PhoneNumber {get; set;}
        public string Status {get; set;}
        public double Total {get; set;}
        public int Quantity {get; set;}
    }
}