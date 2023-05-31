namespace EcommerceShop.Contracts.Dtos
{
    public class RoleSelectedDto
    {
        public string RoleId {get; set;}
        public string RoleName {get; set;}
        public bool Selected {get; set;}
        public object Select()
        {
            throw new NotImplementedException();
        }
    }
}