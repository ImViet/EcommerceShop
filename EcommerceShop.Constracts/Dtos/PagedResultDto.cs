using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Contracts.Dtos
{
    public class PagedResultDto<T>: PagedResultBaseDto
    {
        public List<T> Items { get; set; }
    }
}
