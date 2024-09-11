using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessAccessLayer.DTOS.ReviewDtos;
using Stripe;

namespace BusinessAccessLayer.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile() {
            CreateMap<BusinessAccessLayer.DTOS.ReviewDtos.AddReview, DataAccessLayer.Data.Models.Review>()
           .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<DataAccessLayer.Data.Models.Review, BusinessAccessLayer.DTOS.ReviewDtos.AddReview>();

        }
    }
}
