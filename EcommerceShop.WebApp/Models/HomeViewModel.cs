using EcommerceShop.Contracts.Dtos.CategoryDtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;

namespace EcommerceShop.WebApp.Models
{
    public class HomeViewModel
    {
        public List<ProductDto> Products {get; set;}
        public List<ProductDto> LastestProduct {get; set;}
        public List<CategoryDto> Categories {get; set;}
    }
}