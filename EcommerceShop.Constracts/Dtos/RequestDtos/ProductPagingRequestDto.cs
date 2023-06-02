using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Contracts.Dtos.RequestDtos
{
    public class ProductPagingRequestDto: PagingRequestDto
    {
        public string? search { get; set; }
        public List<int>? CategoryIds { get; set; }
        public string LanguageId {get; set;}
    }
}
