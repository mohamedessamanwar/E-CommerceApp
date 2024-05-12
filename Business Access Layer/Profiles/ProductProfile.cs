using AutoMapper;
using BusinessAccessLayer.DTOS.ProductDtos;
using DataAccessLayer.Data.Models;
namespace BusinessAccessLayer.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductWithCategoryDto>()
             .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.Category.Id))
             .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
             .ReverseMap();
            CreateMap<Product, CreateProductDto>()
            .ReverseMap();
            CreateMap<Product, ViewProduct>().ReverseMap();
            CreateMap<ProductWithCategory, ProductWithCategoryDtoProcudere>().ReverseMap();
        }

    }
}
