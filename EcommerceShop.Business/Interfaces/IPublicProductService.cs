using EcommerceShop.Contracts.Dtos;
using EcommerceShop.Contracts.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Business.Interfaces
{
    public interface IPublicProductService
    {
        Task<PagedResultDto<ProductDto>> GetAllByCategoryIdAsync(int categoryId, int pageIndex, int pageSize);
    }
}
