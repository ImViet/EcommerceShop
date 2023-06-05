using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.CategoryDtos;
using EcommerceShop.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceShop.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly EcommerceShopDbContext _context;
        public CategoryService(EcommerceShopDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<List<CategoryDto>>> GetAll(string languageId)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.CategoryId equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select new {c, ct};
            var categories = await query.Select(x => new CategoryDto(){
                CategoryId = x.c.CategoryId,
                CategoryName = x.ct.Name,
            }).ToListAsync();
            return new ApiSuccessResponse<List<CategoryDto>>(categories);
        }
    }
}