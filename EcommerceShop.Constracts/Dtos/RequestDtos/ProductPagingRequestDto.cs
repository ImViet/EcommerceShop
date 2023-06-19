using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EcommerceShop.Contracts.Dtos.RequestDtos
{
    public class ProductPagingRequestDto: PagingRequestDto
    {
        public string? Search { get; set; }
        public int? CategoryId { get; set; }
        public string LanguageId {get; set;}
        public string? SortOrder {get; set;}
    }
}
