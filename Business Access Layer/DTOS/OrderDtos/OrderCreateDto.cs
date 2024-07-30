using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOS.OrderDtos
{
    public class OrderCreateDto
    {
        [Required]
        public int AddressId { get; set; }
        [Required]
        public string OrderPaymentType { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
    }
}
