using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Contracts.Exceptions
{
    public class EcommerceShopException: Exception
    {
        public EcommerceShopException() 
        {
        }
        public EcommerceShopException(string message) : base(message) 
        {
        }
        public EcommerceShopException(string message, Exception inner): base(message, inner) 
        {
        }
    }
}
