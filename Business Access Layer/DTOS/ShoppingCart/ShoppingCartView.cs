using BusinessAccessLayer.DTOS.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOS.ShoppingCart
{
    public class ShoppingCartView
    {
        public List<ProductShoppingCartView> productShoppingCartViews { get; set; } = new List<ProductShoppingCartView>();       
        public decimal SubTotal { get; set; }

        public int ItemCount {  get; set; } 

    }
}
