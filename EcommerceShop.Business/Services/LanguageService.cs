using AutoMapper;
using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.LanguageDtos;
using EcommerceShop.Data.Data;
using Microsoft.EntityFrameworkCore;
namespace EcommerceShop.Business.Services
{
    public class LanguageService: ILanguageService
    {
        private readonly IMapper _mapper;
        private readonly EcommerceShopDbContext _context;
        public LanguageService(IMapper mapper, EcommerceShopDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ApiResponse<List<LanguageDto>>> GetAllAsync()
        {
            var languages = await _context.Languages.ToListAsync();
            if(languages == null)
                return new ApiErrorResponse<List<LanguageDto>>("Không tìm thấy ngôn ngữ nào");
            var result = _mapper.Map<List<LanguageDto>>(languages);
            return new ApiSuccessResponse<List<LanguageDto>>(result);
        }
    }
}