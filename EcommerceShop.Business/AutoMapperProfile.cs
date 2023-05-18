using EcommerceShop.Contracts.Dtos.AuthDtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using EcommerceShop.Contracts.Dtos.ProductImageDtos;
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
        }
        private void FromDataAccessorLayer()
        {
            //ProductImage
            CreateMap<ProductImage, ProductImageDto>();
        }
        
    }
}
