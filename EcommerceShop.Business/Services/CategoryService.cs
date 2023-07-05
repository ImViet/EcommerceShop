using AutoMapper;
using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos.CategoryDtos;
using EcommerceShop.Contracts.Exceptions;
using EcommerceShop.Data.Data;
using EcommerceShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcommerceShop.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly EcommerceShopDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(EcommerceShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<CategoryDto>>> GetAllAsync(string languageId)
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
        public async Task<ApiResponse<CategoryDto>> GetByIdAsync(int categoryId, string languageId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if(category == null)
            {
                throw new EcommerceShopException("Không tìm thấy loại sản phẩm");
            }
            var categoryTranslation = await _context.CategoryTranslations
                                .FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.LanguageId == languageId);
            var categoryResult = new CategoryDto()
            {
                CategoryId = categoryId,
                CategoryName = categoryTranslation.Name,
                SeoAlias = categoryTranslation.SeoAlias,
                SeoTitle = categoryTranslation.SeoTitle,
                SeoDescription = categoryTranslation.SeoDescription
            };
            return new ApiSuccessResponse<CategoryDto>(categoryResult);
        }
        public async Task<ApiResponse<bool>> CreateCategoryAsync(CategoryCreateDto newCategory)
        {
            var category = _mapper.Map<Category>(newCategory);
            category.CategoryTranslations = new List<CategoryTranslation>()
            {
                new CategoryTranslation()
                {
                    Name = newCategory.Name,
                    SeoDescription = newCategory.SeoDescription,
                    SeoAlias = newCategory.SeoAlias,
                    SeoTitle = newCategory.SeoTitle,
                    LanguageId = newCategory.LanguageId
                }
            };
            await _context.Categories.AddAsync(category);
            var result = await _context.SaveChangesAsync();
            if(result > 0)
                return new ApiSuccessResponse<bool>();
            return new ApiErrorResponse<bool>("Tạo danh mục thất bại");
        }

        public async Task<ApiResponse<bool>> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _context.Categories.FindAsync(categoryUpdateDto.CategoryId);
            var categoryTranslation = await _context.CategoryTranslations
                                                .FirstOrDefaultAsync(x => x.CategoryId == categoryUpdateDto.CategoryId && x.LanguageId == categoryUpdateDto.LanguageId);
            if(category == null || categoryTranslation == null)
            {
                throw new EcommerceShopException($"Không thể tìm thấy danh mục với Id: {categoryUpdateDto.CategoryId}");
            }
            _mapper.Map(categoryUpdateDto, categoryTranslation);
            _context.CategoryTranslations.Update(categoryTranslation);
            var result = await _context.SaveChangesAsync();
            if(result > 0)
                return new ApiSuccessResponse<bool>();
            return new ApiErrorResponse<bool>("Cập nhật danh mục thất bại");
        }

        public async Task<ApiResponse<bool>> DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if(category == null)
            {
                throw new EcommerceShopException($"Không tìm thấy danh mục với Id: {categoryId}");
            }
            _context.Categories.Remove(category);
           var result = await _context.SaveChangesAsync();
            if(result > 0)
                return new ApiSuccessResponse<bool>();
            return new ApiErrorResponse<bool>("Xoá sản phẩm thất bại");
        }
    }
}