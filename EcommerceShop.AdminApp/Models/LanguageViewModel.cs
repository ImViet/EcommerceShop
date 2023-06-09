using EcommerceShop.Contracts.Dtos.LanguageDtos;

namespace EcommerceShop.AdminApp.Models
{
    public class LanguageViewModel
    {
        public List<LanguageDto> Languages {get; set;}
        public string CurrentLanguageId {get; set;}
        public string CurrentUrl {get; set;}
    }
}