using EcommerceShop.AdminApp.Controllers;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.AdminApp.Controllers
{
    public class UserController: BaseController
    {
        private readonly IUserApiService _userService;
        public UserController(IUserApiService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index(string search = null, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetUserPagingRequestDto()
            {
                search = search,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
            var users = await _userService.GetAllUser(request);
            return View(users);
        }
    }
}