using AutoMapper;
using BusinessAccessLayer.DTOS.AddressDtos;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile() { 
            CreateMap<CreateAddressDto,Address>().ReverseMap();
            CreateMap<Address,ViewAddressDto>().ReverseMap();
        }
    }
}
