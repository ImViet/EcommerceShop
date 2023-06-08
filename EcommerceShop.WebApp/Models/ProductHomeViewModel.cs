using EcommerceShop.Contracts.Dtos.ProductDtos;

namespace EcommerceShop.WebApp.Models
{
    public class ProductHomeViewModel
    {
        public List<ProductDto> Products {get; set;}
        public List<ProductDto> LastestProduct {get; set;}
    }
}