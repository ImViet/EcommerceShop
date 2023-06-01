using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.LanguageDtos;

namespace EcommerceShop.AdminApp.Interfaces
{
    public interface ILanguageApiService
    {
        Task<ApiResponse<List<LanguageDto>>> GetAll();
    }
}