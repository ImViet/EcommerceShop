using AutoMapper;
using EcommerceShop.Business.Interfaces;
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

        public async Task<PagedResultDto<UserDto>> GetAllUserAsync(GetUserPagingRequestDto request)
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
                TotalRecord = totalRow,
                Items = _mapper.Map<List<UserDto>>(data)
            };
            return pagedResult;
        }

        public async Task<UserDto> GetUserByIdAsync(Guid userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if(user == null)
            {
                throw new EcommerceShopException($"Cannot find user with id = {userId}");
            }
            return _mapper.Map<UserDto>(user);
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
    }
}