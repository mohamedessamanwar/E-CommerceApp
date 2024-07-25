using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Product  Product { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedTime { get; set; }


    }
}
