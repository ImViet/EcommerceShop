namespace EcommerceShop.Contracts.Dtos.UserDtos
{
    public class UserUpdateDto
    {
        public Guid Id {get; set;}
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DoB { get; set; }
        public string? PhoneNumber { get; set; }
    }
}