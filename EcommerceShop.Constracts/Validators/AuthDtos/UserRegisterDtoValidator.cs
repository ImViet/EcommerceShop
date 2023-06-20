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
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Nhập tên tài khoản");
            //    .Custom(async (userName, context) =>
            //{
            //    var result = await _userService.IsUserNameUniqueAsync(userName);
            //    if (result)
            //        context.AddFailure("Username is already");
            //});

            RuleFor(x => x.Password).NotEmpty().WithMessage("Nhập mật khẩu")
                .MinimumLength(6).WithMessage("Mật khẩu tối thiểu 6 ký tự");

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Xác nhận mật khẩu")
                .MinimumLength(6).WithMessage("Mật khẩu tối thiểu 6 ký tự")
                .Equal(x => x.Password).WithMessage("Không trùng khớp với mật khẩu");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Nhập email")
                .EmailAddress().WithMessage("Không đúng định dạng email");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Nhập tên")
                .MaximumLength(100).WithMessage("Tối đa 100 ký tự");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Nhập họ")
                .MaximumLength(100).WithMessage("Tối đa 100 ký tự");

            RuleFor(x => x.DoB).NotEmpty().WithMessage("Nhập ngày tháng năm sinh")
                .LessThan(DateTime.Now.AddYears(-14)).WithMessage("Người dùng không đủ 14 tuổi");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Nhập số điện thoại")
                .Matches(new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")).WithMessage("SĐT không hợp lệ")
                .MaximumLength(10).WithMessage("Chỉ 10 chữ số")
                .MinimumLength(10).WithMessage("Chỉ 10 chữ số");
        }
    }
}
