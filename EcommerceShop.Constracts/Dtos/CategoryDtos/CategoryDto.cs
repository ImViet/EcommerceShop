namespace EcommerceShop.Contracts.Dtos.CategoryDtos
{
    public class CategoryDto
    {
        public int CategoryId {get; set;}
        public string CategoryName {get; set;}
        public string? SeoDescription { set; get; }
        public string? SeoTitle { set; get; }
        public string? SeoAlias { set; get; }
    }
}