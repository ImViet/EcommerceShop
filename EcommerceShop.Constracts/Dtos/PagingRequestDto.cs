using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Contracts.Dtos
{
    public class PagingRequestDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
