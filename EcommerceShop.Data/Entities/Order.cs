using EcommerceShop.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Data.Entities
{
    public class Order
    {
        public Guid OrderId { set; get; }
        public DateTime OrderDate { set; get; }
        public Guid? UserId { set; get; }
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }
        public string PaymentBy {get; set;}
        public OrderStatus Status { set; get; }
        public List<OrderDetail> OrderDetails { get; set; }

        public AppUser? User { set; get; }
    }
}
