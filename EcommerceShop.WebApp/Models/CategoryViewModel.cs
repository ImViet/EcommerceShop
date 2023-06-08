using EcommerceShop.Contracts.Dtos.CategoryDtos;

namespace EcommerceShop.WebApp.Models
{
    public class CategoryViewModel
    {
        public List<CategoryDto> Categories {get; set;}
        public int CurrentCategoryId {get; set;}
    }
}