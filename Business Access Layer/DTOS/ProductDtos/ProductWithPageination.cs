using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer.DTOS.ProductDtos
{
    public class ProductWithPageination
    {
        public int Count { get; set; }
        public List<ProductViewPagination> Products { get; set; }
    }
}
