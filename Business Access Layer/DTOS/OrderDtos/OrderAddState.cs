using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOS.OrderDtos
{
    public class OrderAddState
    {
        public bool State { get; set; }
        public string? Type { get; set; } 
        public string Massage {  get; set; }
        public string? SessionUrl {  get; set; }


    }
}
