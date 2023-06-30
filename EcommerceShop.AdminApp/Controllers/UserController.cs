using EcommerceShop.AdminApp.Controllers;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using EcommerceShop.Contracts.Dtos.RoleDtos;
using EcommerceShop.Contracts.Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.AdminApp.Controllers
{
    [Authorize(Policy = "RoleAdmin")]
    public class UserController: BaseController
    {
        private readonly IUserApiService _userService;
        private readonly IRoleApiService _roleService;

        public UserController(IUserApiService userService, IRoleApiService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        public async Task<IActionResult> Index(string search = null, int pageIndex = 1, int pageSize = 3)
        {
            var request = new GetUserPagingRequestDto()
            {
                search = search,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
            var data = await _userService.GetAllUser(request);
            ViewData["ListUsers"] = data.ResponseObject.Items;
            ViewData["searchKeyword"] = search;
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
                TempData["ModalSuccess"] = "Tạo thành công";
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", result.Message);
            return View(userRegister);
        }
        [HttpPost]
        public async Task<ViewComponentResult> GetUser(Guid userId)
        {
            var data = await _userService.GetUser(userId);
            return ViewComponent("UserDetail", data.ResponseObject);
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
                TempData["ModalSuccess"] = "Cập nhật thành công";
                return RedirectToAction("Index", "User");
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
        [HttpPost]
        public async Task<ViewComponentResult> GetRole(Guid userId)
        {
            var roleAssigned = await GetRoleAssigned(userId);
            return ViewComponent("RoleAssign", roleAssigned);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(RoleAssignDto roleAssign)
        {
            if(!ModelState.IsValid)
                return View();
            var data = await _userService.AssignRole(roleAssign.UserId, roleAssign);
            if(data.IsSuccessed)
            {
                TempData["ModalSuccess"] = "Cập nhật quyền thành công";
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", data.Message);
            return View();
        }
        private async Task<RoleAssignDto> GetRoleAssigned(Guid userId)
        {
            var user = await _userService.GetUser(userId);
            var roles = await _roleService.GetAllRole();
            var roleAssigned = new RoleAssignDto();
            foreach (var role in roles.ResponseObject)
            {
                roleAssigned.Roles.Add(new SelectedDto()
                {
                    Id = role.RoleId.ToString(),
                    Name = role.RoleName,
                    Selected = user.ResponseObject.Roles.Contains(role.RoleName)
                });
            }
            return roleAssigned;
        }
    }
}