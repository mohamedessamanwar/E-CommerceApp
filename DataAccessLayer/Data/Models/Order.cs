using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string OrderStatus { get; set; }
        public  string OrderPaymentStatus { get; set; }
        public string OrderPaymentType { get; set; }
        public string UserId { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ApplicationUser User { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }    
        public double OrderTotal { get; set; }
        public IEnumerable<OrderDetail> orderDetails { get; set; }

    }
}
