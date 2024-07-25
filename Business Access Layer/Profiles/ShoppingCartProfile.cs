using AutoMapper;
using Business_Access_Layer.DTOS.ShoppingCart;
using BusinessAccessLayer.DTOS.ShoppingCart;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Profiles
{
    public class ShoppingCartProfile : Profile
    {
        public  ShoppingCartProfile()
        {
            CreateMap<ShoppingCart,ShoppingCartCreateView>().ReverseMap();
        }
    }
}
