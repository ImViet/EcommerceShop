using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.LanguageDtos;

namespace EcommerceShop.WebApp.Interfaces
{
    public interface ILanguageApiService
    {
        Task<ApiResponse<List<LanguageDto>>> GetListlanguage();
    }
}