using AutoMapper;
using BusinessAccessLayer.DTOS.AddressDtos;
using BusinessAccessLayer.DTOS.OrderDtos;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Profiles
{
    public class OrderProfile : Profile  
    {
        public OrderProfile() {
            CreateMap<OrderCreateDto,Order>().ReverseMap();


        }
    }
}
