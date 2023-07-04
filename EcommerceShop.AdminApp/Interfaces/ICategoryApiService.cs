using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.CategoryDtos;

namespace EcommerceShop.AdminApp.Interfaces
{
    public interface ICategoryApiService
    {
        Task<ApiResponse<List<CategoryDto>>> GetListCategory(string languageId);
        Task<ApiResponse<bool>> CreateCategory(CategoryCreateDto category);
        Task<ApiResponse<bool>> UpdateCategory(CategoryUpdateDto category);
        Task<ApiResponse<bool>> DeleteCategory(int categoryId);
    }
}