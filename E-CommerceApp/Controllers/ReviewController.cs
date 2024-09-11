using BusinessAccessLayer.DTOS.AddressDtos;
using BusinessAccessLayer.DTOS.Response;
using BusinessAccessLayer.DTOS.ReviewDtos;
using BusinessAccessLayer.Services.AddressService;
using BusinessAccessLayer.Services.Reviewervice;
using BusinessAccessLayer.Validations.ReviewValidation;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_CommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : BaseController
    {
        private readonly IValidator<AddReview> validator;
        private readonly IReviewService reviewService;
        public ReviewController(IReviewService reviewService , IValidator<AddReview> validator )
        {
            this.reviewService = reviewService;
            this.validator = validator;
        }
        [HttpPost]
        [Authorize]

        public async Task<IActionResult> CreateReview(AddReview addReview)
        {
            var validator = this.validator.Validate(addReview);
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            addReview.UserId = userIdFromToken;

            var result = await reviewService.CreateReview(addReview);
            if (result.Status == false)
            {
                return NewResult(new ResponseHandler().BadRequest<CreateAddressDto>(result.Massage));

            }
            return NewResult(new ResponseHandler().Created<AddReview>(addReview));

        }
        [HttpGet]
        [Route("/{ProductId}")]
        public async Task<IActionResult> ViewReview(int ProductId)
        {
            
            var result = await reviewService.ViewReviews(ProductId);
            
            return NewResult(new ResponseHandler().Success<ViewReview>(result));

        }


    }
}
