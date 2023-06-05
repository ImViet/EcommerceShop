using System.Text.RegularExpressions;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using FluentValidation;

namespace EcommerceShop.Contracts.Validators.ProductDtos
{
    public class ProductCreateDtoValidator: AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nhập tên sản phẩm");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Nhập giá bán");
            RuleFor(x => x.OriginalPrice).NotEmpty().WithMessage("Nhập giá gốc");
            RuleFor(x => x.Details).NotEmpty().WithMessage("Nhập chi tiết");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Nhập mô tả");
            RuleFor(x => x.SeoAlias).NotEmpty().WithMessage("Nhập Seo alias");
            RuleFor(x => x.SeoTitle).NotEmpty().WithMessage("Nhập Seo title");
            RuleFor(x => x.SeoDescription).NotEmpty().WithMessage("Nhập mô tả Seo");
        }
    }
}