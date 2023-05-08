using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Business.Interfaces
{
    public interface IManageProductService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<PagingDto<ProductDto>> GetAllPagingAsync(string search, int pageIndex, int pageSize);
        Task<int> CreateProductAsync(ProductCreateDto productCreateDto);
        Task<int> UpdateProductAsync(ProductUpdateDto productUpdateDto);
        Task<int> DeleteProductAsync();
    }
}
