using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOS.OrderDtos
{
    public class OrderPaymentStatus
    {
        public Boolean Status { get; set; }
        [Required]
        public string StatusMessage { get; set; }

    }
}
