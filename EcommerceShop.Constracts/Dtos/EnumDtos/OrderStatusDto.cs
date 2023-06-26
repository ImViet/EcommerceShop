using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Contracts.Dtos.EnumDtos
{
    public enum OrderStatusDto
    {
        InProgress,
        Confirmed,
        Shipping,
        Success,
        Canceled,
        Error,
        Paying
    }
}
