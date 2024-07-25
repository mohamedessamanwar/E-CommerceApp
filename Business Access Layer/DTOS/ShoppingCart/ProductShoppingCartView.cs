using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOS.ShoppingCart
{
    public class ProductShoppingCartView
    {
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; }
        public decimal Price { get; set; }
         
        public List<string?> Urls { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; } = string.Empty;
        public int ItemCount { get; set; }
    }
}
