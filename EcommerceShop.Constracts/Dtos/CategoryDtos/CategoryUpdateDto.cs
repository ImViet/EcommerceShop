using EcommerceShop.Contracts.Dtos.EnumDtos;

namespace EcommerceShop.Contracts.Dtos.CategoryDtos
{
    public class CategoryUpdateDto
    {
        public int CategoryId {get; set;}
        public string? Name {get; set;}
        public string? SeoDescription { set; get; }
        public string? SeoTitle { set; get; }
        public string? LanguageId { set; get; }
        public string? SeoAlias { set; get; }
    }
}