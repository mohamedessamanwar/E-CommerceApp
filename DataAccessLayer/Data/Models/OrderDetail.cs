using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Models
{
    public class OrderDetail : BaseEntity
    {
        public int Id { get; set; }
   
        public int OrderId { get; set; }
        public Order Order { get; set; }
  
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public double Price { get; set; } // current 
    }
}
