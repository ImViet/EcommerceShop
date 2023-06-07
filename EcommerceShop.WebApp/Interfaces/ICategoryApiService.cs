using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.CategoryDtos;

namespace EcommerceShop.WebApp.Interfaces
{
    public interface ICategoryApiService
    {
        Task<ApiResponse<List<CategoryDto>>> GetListCategory(string languageId);
    }
}