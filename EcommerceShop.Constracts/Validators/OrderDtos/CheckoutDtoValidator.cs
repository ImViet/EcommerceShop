using System.Text.RegularExpressions;
using EcommerceShop.Contracts.Dtos.OrderDtos;
using FluentValidation;

namespace EcommerceShop.Contracts.Validators.OrderDtos
{
    public class CheckoutDtoValidator: AbstractValidator<CheckoutDto>
    {
        public CheckoutDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Nhập tên");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Nhập họ");
            RuleFor(x => x.ShipAddress).NotEmpty().WithMessage("Nhập địa chỉ");
            RuleFor(x => x.ShipEmail).NotEmpty().WithMessage("Nhập email");
            RuleFor(x => x.ShipPhoneNumber).NotEmpty().WithMessage("Nhập số điện thoại")
                .Matches(new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")).WithMessage("SĐT không hợp lệ")
                .MaximumLength(10).WithMessage("Chỉ 10 chữ số")
                .MinimumLength(10).WithMessage("Chỉ 10 chữ số");
            RuleFor(x => x.PaymentBy).NotEmpty().WithMessage("Chọn phương thức thanh toán");
            RuleFor(x => x.ShipProvince).NotEmpty().WithMessage("Chọn tỉnh");
            RuleFor(x => x.ShipDistrict).NotEmpty().WithMessage("Chọn huyện");
            RuleFor(x => x.ShipWard).NotEmpty().WithMessage("Chọn xã");
        }
    }
}