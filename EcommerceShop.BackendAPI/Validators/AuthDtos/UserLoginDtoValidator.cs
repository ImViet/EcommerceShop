using EcommerceShop.Contracts.Dtos.AuthDtos;
using FluentValidation;

namespace EcommerceShop.BackendAPI.Validators.AuthDtos
{
    public class UserLoginDtoValidator: AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator() 
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Username is required")
                 .MinimumLength(6).WithMessage("Password is at least 6 characters");
        }
    }
}
