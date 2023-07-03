using EcommerceShop.Contracts.Dtos.CartDtos;

namespace EcommerceShop.Contracts.Dtos.OrderDtos
{
    public class OrderCreateDto
    {
        public Guid OrderId {get; set;}
        public string? FirstName {get; set;}
        public string? LastName {get; set;}
        public string? ShipAddress { set; get; }
        public string? ShipEmail { set; get; }
        public string? ShipPhoneNumber { set; get; }
        public string? PaymentBy {get; set;}
        public string? ShipProvince {get; set;}
        public string? ShipDistrict {get; set;}
        public string? ShipWard {get; set;}
        public List<CartDto> Carts {get; set;}
    }
}