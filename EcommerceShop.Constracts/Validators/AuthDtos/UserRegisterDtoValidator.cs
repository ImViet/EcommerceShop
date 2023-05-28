//using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using FluentValidation;
using System.Text.RegularExpressions;

namespace EcommerceShop.Contracts.Validators.AuthDtos
{
    public class UserRegisterDtoValidator: AbstractValidator<UserRegisterDto>
    {
        //private readonly IUserService _userService;
        public UserRegisterDtoValidator() 
        {
            //_userService = userService;
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
            //    .Custom(async (userName, context) =>
            //{
            //    var result = await _userService.IsUserNameUniqueAsync(userName);
            //    if (result)
            //        context.AddFailure("Username is already");
            //});

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password is at least 6 characters");

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirmpassword is required")
                .MinimumLength(6).WithMessage("Confirmpassword is at least 6 characters")
                .Equal(x => x.Password).WithMessage("Confirmpassword is not match password");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email is required");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Firstname is required")
                .MaximumLength(100).WithMessage("Firstname must be 100 characters or fewer");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Lastname is required")
                .MaximumLength(100).WithMessage("Lastname must be 100 characters or fewer");

            RuleFor(x => x.DoB).NotEmpty().WithMessage("Date of birth is required")
                .LessThan(DateTime.Now.AddYears(-14)).WithMessage("User is under 14");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phonenumber is required")
                .MaximumLength(10).WithMessage("Phonenumber is 10 numbers")
                .MinimumLength(10).WithMessage("Phonenumber is 10 numbers")
                .Matches(new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")).WithMessage("Phonenumber not valid");
        }
    }
}
