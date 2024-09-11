using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Models
{
    public class UserToken : BaseEntity
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string UserEmail { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser  { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
