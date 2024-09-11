using AutoMapper;
using BusinessAccessLayer.Services.Reviewervice;
using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.DTOS.ReviewDtos;
using DataAccessLayer.Data.Models;
using DataAccessLayer.UnitOfWorkRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessAccessLayer.Services.Reviewervice
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork unitOfWork ;
        private readonly IMapper mapper ;
        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        public async Task<CreateStatus<AddReview>> CreateReview(AddReview addReview)
        {
            //var review = new Review()
            //{
            //    CreatedDate = DateTime.Now,
            //    Comment = addReview.Comment,
            //    Rate = addReview.Rate,
            //    UserId = addReview.UserId,
            //    ProductId = addReview.ProductId
            //};
            var review = this.mapper.Map<Review>(addReview);
          //  review.CreatedDate = DateTime.Now;
            await unitOfWork.reviewRepo.AddAsync(review);
            int result = unitOfWork.Complete();
            if (result == 0) {
                return new CreateStatus<AddReview>()
                {
                    Status = false,
                    Massage = "fiald to add review",
                    Value = addReview 
                };
            }
            return new CreateStatus<AddReview>()
            {
                Status = true,
                Massage = "add review",
                Value = addReview
            };

        }

        public async Task<ViewReview> ViewReviews(int product) 
            {
              var reviews = await unitOfWork.reviewRepo.GetReviewByProduct(product);
            return new ViewReview()
            {
                reviewGroupDtos = reviews.GroupBy(r => r.Rate).Select(g => new ReviewGroupDto
                {
                    Rate = g.Key,
                    Count = g.Count()
                }).ToList(),

                reviewsUsers = reviews.Select(r => new ReviewsUser
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    UserName = r.User.UserName , 
                    Rate = r.Rate , 
                    Comment = r.Comment , 

                    

                }).ToList() , 
                AvgRate = (decimal)reviews.Average(r => r.Rate) 
            };

            } 
    }
}
