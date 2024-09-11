using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOS.ReviewDtos
{
    public class AddReview
    {
        
        public string? UserId { get; set; }
   
        public int? ProductId { get; set; }

       // [Range(0, 5)]
        public int? Rate { get; set; }
      //  [MaxLength(500)]
        public string? Comment { get; set; }



    }
}
