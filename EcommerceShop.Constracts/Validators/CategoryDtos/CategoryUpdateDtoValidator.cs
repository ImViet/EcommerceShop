using EcommerceShop.Contracts.Dtos.CategoryDtos;
using FluentValidation;

namespace EcommerceShop.Contracts.Validators
{
    public class CategoryUpdateDtoValidator: AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nhập tên loại sản phẩm");
            RuleFor(x => x.SeoTitle).NotEmpty().WithMessage("Nhập seo title");
            RuleFor(x => x.SeoAlias).NotEmpty().WithMessage("Nhập seo alias");
            RuleFor(x => x.SeoDescription).NotEmpty().WithMessage("Nhập mô tả seo");
        }
    }
}