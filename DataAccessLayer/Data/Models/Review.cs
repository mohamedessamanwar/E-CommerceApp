using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Models
{
    public class Review : BaseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Range(0, 5)]
        public int Rate { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? Liked { get; set; }
        
    }
}
