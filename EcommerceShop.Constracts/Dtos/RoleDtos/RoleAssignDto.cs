using EcommerceShop.Contracts.Dtos;
namespace EcommerceShop.Contracts.Dtos.RoleDtos
{
    public class RoleAssignDto
    {
        public Guid UserId {get; set;}
        public List<SelectedDto> Roles {get; set;} = new List<SelectedDto>();
    }
}