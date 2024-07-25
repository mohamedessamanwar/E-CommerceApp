using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Models
{
    public class Address
    {
        public  int Id { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string State { get; set; }
    }
}
