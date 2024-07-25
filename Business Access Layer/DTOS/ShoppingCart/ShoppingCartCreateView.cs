using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOS.ShoppingCart
{
    public class ShoppingCartCreateView
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Count { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
        
    }
}
