using System.Text.RegularExpressions;
using EcommerceShop.Contracts.Dtos.UserDtos;
using FluentValidation;

namespace EcommerceShop.Contracts.Validators.UserDtos
{
    public class UserUpdateDtoValidator: AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Nhập email")
                .EmailAddress().WithMessage("Không đúng định dạng email");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Nhập tên")
                .MaximumLength(100).WithMessage("Tối đa 100 ký tự");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Nhập họ")
                .MaximumLength(100).WithMessage("Tối đa 100 ký tự");

            RuleFor(x => x.DoB).NotEmpty().WithMessage("Nhập ngày tháng năm sinh")
                .LessThan(DateTime.Now.AddYears(-14)).WithMessage("Người dùng không đủ 14 tuổi");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Nhập số điện thoại")
                .MaximumLength(10).WithMessage("Chỉ 10 chữ số")
                .MinimumLength(10).WithMessage("Chỉ 10 chữ số")
                .Matches(new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")).WithMessage("SĐT không hợp lệ");
        }
    }
}