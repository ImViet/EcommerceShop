using EcommerceShop.Contracts.Dtos.AuthDtos;
using FluentValidation;

namespace EcommerceShop.Contracts.Validators.AuthDtos
{
    public class UserLoginDtoValidator: AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator() 
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Nhập tên đăng nhập");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Nhập mật khẩu")
                 .MinimumLength(6).WithMessage("Mật khẩu ít nhất 6 ký tự");
        }
    }
}
