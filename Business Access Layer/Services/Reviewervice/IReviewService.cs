using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.DTOS.ReviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services.Reviewervice
{
    public interface IReviewService
    {
        Task<CreateStatus<AddReview>>CreateReview(AddReview addReview);
        Task<ViewReview> ViewReviews(int product); 
    }
}
