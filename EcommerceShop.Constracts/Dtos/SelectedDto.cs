namespace EcommerceShop.Contracts.Dtos
{
    public class SelectedDto
    {
        public string Id {get; set;}
        public string Name {get; set;}
        public bool Selected {get; set;}
        public object Select()
        {
            throw new NotImplementedException();
        }
    }
}