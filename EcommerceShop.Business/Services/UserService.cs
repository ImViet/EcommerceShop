using AutoMapper;
using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.RequestDtos;
using EcommerceShop.Contracts.Dtos.UserDtos;
using EcommerceShop.Contracts.Exceptions;
using EcommerceShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceShop.Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PagedResultDto<UserDto>>> GetAllUserAsync(GetUserPagingRequestDto request)
        {
            var query = _userManager.Users;
            //2.Filter
            if(!string.IsNullOrEmpty(request.search))
            {
                query = query.Where(x => x.UserName.Contains(request.search) || x.PhoneNumber.Contains(request.search));
            }

            //3.Paging
            var totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex-1)*request.PageSize)
                                    .Take(request.PageSize)
                                    .ToListAsync();
            var pagedResult = new PagedResultDto<UserDto>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = _mapper.Map<List<UserDto>>(data)
            };
            return new ApiSuccessResponse<PagedResultDto<UserDto>>(pagedResult);
        }

        public async Task<ApiResponse<UserDto>> GetUserByIdAsync(Guid userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if(user == null)
            {
                throw new EcommerceShopException($"Cannot find user with id = {userId}");
            }
            return new ApiSuccessResponse<UserDto>(_mapper.Map<UserDto>(user));
        }

        public async Task<bool> IsUserNameUniqueAsync(string userName)
        {
            var checkUserName = await _userManager.FindByNameAsync(userName);
            if(checkUserName == null)
            {
                return false;
            }
            return true;
        }

        public async Task<ApiResponse<bool>> UpdateUserAsync(Guid userId, UserUpdateDto userUpdate)
        {
            var checkEmail = await _userManager.Users.AnyAsync(x => x.Email == userUpdate.Email && x.Id != userId);
            if(checkEmail)
            {
                return new ApiErrorResponse<bool>("Email đã tồn tại");
            }
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if(user == null)
                return new ApiErrorResponse<bool>("Nguời dùng không tồn tại");
            _mapper.Map(userUpdate, user);
            var result = await _userManager.UpdateAsync(user);
            if(!result.Succeeded)
            {
                return new ApiErrorResponse<bool>("Cập nhật không thành công");
            }
            return new ApiSuccessResponse<bool>();
        }

        public async Task<ApiResponse<bool>> DeleteUserAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if(user == null)
                return new ApiErrorResponse<bool>("Người dùng không tồn tại");
            var result = await _userManager.DeleteAsync(user);
            if(!result.Succeeded)
                return new ApiErrorResponse<bool>("Xoá thất bại");
            return new ApiSuccessResponse<bool>();
        }

    }
}