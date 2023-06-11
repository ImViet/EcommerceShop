using FluentValidation;
using EcommerceShop.Contracts.Dtos.ProductDtos;
namespace EcommerceShop.Contracts.Dtos.ProductDtos
{
    public class ProductUpdateDtoValidator: AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nhập tên sản phẩm");
            RuleFor(x => x.Details).NotEmpty().WithMessage("Nhập chi tiết");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Nhập mô tả");
            RuleFor(x => x.SeoAlias).NotEmpty().WithMessage("Nhập Seo alias");
            RuleFor(x => x.SeoTitle).NotEmpty().WithMessage("Nhập Seo title");
            RuleFor(x => x.SeoDescription).NotEmpty().WithMessage("Nhập mô tả Seo");
        }
    }
}