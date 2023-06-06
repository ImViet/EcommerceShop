namespace EcommerceShop.Contracts.Dtos.CategoryDtos
{
    public class CategoryAssignDto
    {
        public int ProductId {get; set;}
        public List<SelectedDto> Categories {get; set;} = new List<SelectedDto>();
    }
}