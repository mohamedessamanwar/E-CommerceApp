using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOS.OrderDtos
{
    public class EncryptedOrderCreateDto
    {
        public string EncryptedOrderData { get; set; }
    }
    public class OrderCreateDto
    {
        [Required]
        public int AddressId { get; set; }
        [Required]
        public string Token { get; set; }
        //public string CardNumber { get; set; }
        //public long ExpMonth { get; set; }
        //public long ExpYear { get; set; }
        //public string CVC { get; set; }
    }
}
