 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOS
{
    public class PaymentResult
    {
        public string? session {  get; set; }
        public string Massage { get; set; }
        public bool status { get; set; }
    }
}
