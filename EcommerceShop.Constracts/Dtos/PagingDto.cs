using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Contracts.Dtos
{
    public class PagingDto<T>
    {
        public int TotalRecord { get; set; }
        public List<T> Items { get; set; }
    }
}
