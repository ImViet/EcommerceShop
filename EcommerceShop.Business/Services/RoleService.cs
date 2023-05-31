using AutoMapper;
using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.RoleDtos;
using EcommerceShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceShop.Business.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;
        public RoleService(RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<RoleDto>>> GetAllRoleAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if(roles == null)
                return new ApiErrorResponse<List<RoleDto>>("Không tìm thấy quyền");
            var result = _mapper.Map<List<RoleDto>>(roles);
            return new ApiSuccessResponse<List<RoleDto>>(result);
        }
    }
}