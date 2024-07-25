using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOS.AddressDtos
{
    public class ViewAddressDto
    {
        public string Description { get; set; }
        public string City  { get; set; }
    }
}
