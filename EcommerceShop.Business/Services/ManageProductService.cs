using AutoMapper;
using EcommerceShop.Business.Interfaces;
using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using EcommerceShop.Data.Data;
using EcommerceShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Business.Services
{
    public class ManageProductService : IManageProductService
    {
        private readonly EcommerceShopDbContext _context;
        private readonly IMapper _mapper;
        public ManageProductService(EcommerceShopDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<List<ProductDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PagingDto<ProductDto>> GetAllPagingAsync(string search, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateProductAsync(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();

        }
        public Task<int> UpdateProductAsync(ProductUpdateDto product)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteProductAsync()
        {
            throw new NotImplementedException();
        }


    }
}
