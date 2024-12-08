using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer.DTOS.ProductDtos
{
    public class ProductViewPagination
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public List<string>? ImageUrl { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; } = string.Empty;
        public int count { get; set; }
    }
}
