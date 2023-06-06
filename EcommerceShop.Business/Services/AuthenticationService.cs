using AutoMapper;
using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Constants;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using EcommerceShop.Contracts.Exceptions;
using EcommerceShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;
        public AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
            RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<ApiResponse<string>> LoginAsync(UserLoginDto userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin.UserName);
            if(user == null)
            {
                return new ApiErrorResponse<string>("Tài khoản không tồn tại");
            }
            var resultLogin = await _signInManager.PasswordSignInAsync(user, userLogin.Password, userLogin.RememberMe, true);
            if(!resultLogin.Succeeded)
            {
                return new ApiErrorResponse<string>("Sai tài khoản hoặc mật khẩu");
            }
            var token = CreateToken(user);
            return new ApiSuccessResponse<string>(token);
        }

        public async Task<ApiResponse<bool>> RegisterAsync(UserRegisterDto userRegister)
        {
            var userName = await _userManager.FindByNameAsync(userRegister.UserName);
            if(userName != null)
            {
                return new ApiErrorResponse<bool>("Tên tài khoản đã tồn tại");
            }
            var email = await _userManager.FindByEmailAsync(userRegister.Email);
            if(email != null)
            {
                return new ApiErrorResponse<bool>("Email đã tồn tại");
            }
            var user = _mapper.Map<AppUser>(userRegister);
            user.Id = Guid.NewGuid();
            var resultRegister = await _userManager.CreateAsync(user, userRegister.Password);
            if(!resultRegister.Succeeded) 
            {
                return new ApiErrorResponse<bool>("Đăng ký thất bại");
            }
            return new ApiSuccessResponse<bool>();
        }
        private string CreateToken(AppUser user)
        {
            var roles = _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim("Email", user.Email),
                new Claim("UserName", user.UserName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                JWTSettings.Issuer,
                JWTSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
