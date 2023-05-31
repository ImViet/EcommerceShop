using EcommerceShop.Contracts.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.AdminApp.Views.Shared.Components.UserDetail
{
    public class UserDetailViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(UserDto user)
        {
            return View("Default", user);
        }
    }
}