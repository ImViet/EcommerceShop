using EcommerceShop.Contracts.Dtos.ProductDtos;

namespace EcommerceShop.Contracts.Dtos.CartDtos
{
    public class CartDto
    {
        public ProductDto Product {get; set;}
        public int Quantity {get; set;}
        public double Total 
        {
            get { return Quantity * (double)Product.Price;}
        }
    }
}