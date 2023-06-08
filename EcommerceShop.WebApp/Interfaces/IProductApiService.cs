using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.ProductDtos;

namespace EcommerceShop.WebApp.Interfaces
{
    public interface IProductApiService
    {
        Task<ApiResponse<List<ProductDto>>> GetFeatureProduct(string languageId, int categoryId, int take);
        Task<ApiResponse<List<ProductDto>>> GetLastestProduct(string languageId, int take);
    }
}