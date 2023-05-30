using EcommerceShop.AdminApp.Controllers;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using EcommerceShop.Contracts.Dtos.UserDtos;
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
        public async Task<IActionResult> Index(string search = null, int pageIndex = 1, int pageSize = 1, string isActionSuccess = null)
        {
            var request = new GetUserPagingRequestDto()
            {
                search = search,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
            var data = await _userService.GetAllUser(request);
            ViewData["ListUsers"] = data.ResponseObject.Items;
            ViewData["ModalSuccess"] = isActionSuccess;
            return View(data.ResponseObject);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserRegisterDto userRegister)
        {
            if(!ModelState.IsValid)
            {
                return View(userRegister);
            }
            var result = await _userService.CreateUser(userRegister);
            if(result.IsSuccessed)
            {
                return RedirectToAction("Index", "User", new {isActionSuccess = "Tạo thành công"});
            }
            ModelState.AddModelError("", result.Message);
            return View(userRegister);
        }
        [HttpGet]
        public async Task<JsonResult> GetUser(Guid userId)
        {
            var data = await _userService.GetUser(userId);
            return new JsonResult(data.ResponseObject);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid userId)
        {
            var data = await _userService.GetUser(userId);
            var userUpdate = new UserUpdateDto()
            {
                Id = data.ResponseObject.Id,
                FirstName = data.ResponseObject.FirstName,
                LastName = data.ResponseObject.LastName,
                Email = data.ResponseObject.Email,
                PhoneNumber = data.ResponseObject.PhoneNumber,
                DoB = data.ResponseObject.DoB
            };
            return View(userUpdate);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdate)
        {
            if(!ModelState.IsValid)
                return View(userUpdate);
            var data = await _userService.UpdateUser(userUpdate.Id, userUpdate);
            if(data.IsSuccessed)
            {
                return RedirectToAction("Index", "User", new{ isActionSuccess = "Cập nhật thành công" });
            }
            ModelState.AddModelError("", data.Message);
            return View(userUpdate);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid userId)
        {
            if(!ModelState.IsValid)
                return View();
            var data = await _userService.DeleteUser(userId);
            if(data.IsSuccessed)
                return RedirectToAction("Index", "User");
            return View();  
        }
    }
}