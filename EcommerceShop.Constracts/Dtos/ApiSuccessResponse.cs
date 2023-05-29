namespace EcommerceShop.Contracts
{
    public class ApiSuccessResponse<T>: ApiResponse<T>
    {
        public ApiSuccessResponse()
        {
            IsSuccessed = true;
        }
        public ApiSuccessResponse(T responseObject)
        {
            IsSuccessed = true;
            ResponseObject = responseObject;
        }

    }
}