using EcommerceShop.AdminApp.Controllers;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts.Dtos.AuthDtos;
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
            var data = await _userService.GetAllUser(request);
            ViewData["ListUsers"] = data.ResponseObject.Items;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserRegisterDto userRegister)
        {
            if(ModelState.IsValid)
            {
                var result = _userService.CreateUser(userRegister);
                return RedirectToAction("Index", "User");
            }
                return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetUser(Guid userId)
        {
            var data = await _userService.GetUser(userId);
            return new JsonResult(data.ResponseObject);
        }
    }
}