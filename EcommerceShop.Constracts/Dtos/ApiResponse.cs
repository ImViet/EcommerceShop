namespace EcommerceShop.Contracts
{
    public class ApiResponse<T>
    {
        public bool IsSuccessed {get; set;}
        public string Message {get; set;}
        public T ResponseObject {get; set;}
    }
}