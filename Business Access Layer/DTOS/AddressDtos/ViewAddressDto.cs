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
       public int Id { get; set; }
        public string Description { get; set; }
        public string City  { get; set; }
    }
}
