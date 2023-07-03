using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.CategoryDtos;

namespace EcommerceShop.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse<List<CategoryDto>>> GetAllAsync(string languageId);
        Task<ApiResponse<bool>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task<ApiResponse<bool>> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto);
    }
}