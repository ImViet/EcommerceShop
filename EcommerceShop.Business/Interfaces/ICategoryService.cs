using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.CategoryDtos;

namespace EcommerceShop.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse<List<CategoryDto>>> GetAll(string languageId);
    }
}