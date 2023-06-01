using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.LanguageDtos;

namespace EcommerceShop.Business.Interfaces
{
    public interface ILanguageService
    {
        Task<ApiResponse<List<LanguageDto>>> GetAllAsync();
    }
}