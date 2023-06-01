using EcommerceShop.Contracts.Dtos.AuthDtos;
using EcommerceShop.Contracts.Dtos.LanguageDtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using EcommerceShop.Contracts.Dtos.ProductImageDtos;
using EcommerceShop.Contracts.Dtos.RoleDtos;
using EcommerceShop.Contracts.Dtos.UserDtos;
using EcommerceShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Business
{
    public class AutoMapperProfile: AutoMapper.Profile
    {
        public AutoMapperProfile() 
        {
            FromPresentationLayer();
            FromDataAccessorLayer();
        }
        private void FromPresentationLayer()
        {
            //Authenticate
            CreateMap<UserRegisterDto, AppUser>();
            //Product
            CreateMap<ProductCreateDto, Product>()
                .ForMember(d => d.ProductId, t => t.Ignore());
            CreateMap<ProductUpdateDto, ProductTranslation>();
            //ProductImage
            CreateMap<ProductImageCreateDto, ProductImage>();
            CreateMap<ProductImageUpdateDto, ProductImage>();
            //User
            CreateMap<UserUpdateDto, AppUser>()
                .ForMember(d => d.Id, t => t.Ignore());
        }
        private void FromDataAccessorLayer()
        {
            //ProductImage
            CreateMap<ProductImage, ProductImageDto>();
            //User
            CreateMap<AppUser, UserDto>();
            //Role
            CreateMap<AppRole, RoleDto>()
                .ForMember(d => d.RoleId, t => t.MapFrom(src => src.Id))
                .ForMember(d => d.RoleName, t => t.MapFrom(src => src.Name));
            //Language
            CreateMap<Language, LanguageDto>();
        }
        
    }
}
