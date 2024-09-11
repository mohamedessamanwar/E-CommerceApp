using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOS.ReviewDtos
{
    public class ViewReview
    {
        public List<ReviewGroupDto> reviewGroupDtos {  get; set; }
        public List<ReviewsUser> reviewsUsers { get; set; }

        public decimal AvgRate { get; set; }
    }
}
