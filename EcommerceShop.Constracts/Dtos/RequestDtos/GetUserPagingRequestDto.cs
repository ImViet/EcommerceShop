namespace EcommerceShop.Contracts.Dtos.RequestDtos
{
    public class GetUserPagingRequestDto: PagingRequestDto
    {
        public string search { get; set; }
    }
}