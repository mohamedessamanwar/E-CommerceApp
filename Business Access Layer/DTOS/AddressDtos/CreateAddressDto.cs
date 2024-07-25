using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOS.AddressDtos
{
    public class CreateAddressDto
    {
        [Required(ErrorMessage ="description is Required")]
        [MaxLength(255 , ErrorMessage ="max length of description is 255 char")]
        public string Description { get; set; }
        [Required(ErrorMessage = "City is Required")]
        [MaxLength(255, ErrorMessage = "max length of city is 50 char")]
        public string City { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
    }
}
