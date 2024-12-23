﻿using BusinessAccessLayer.DTOS.ShoppingCart;
using BusinessAccessLayer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services.ShoppingCartService
{
    public interface IShoppingCartService
    {
        Task<CreateStatus<ShoppingCartCreateView>> CreateShoppingCart(ShoppingCartCreateView shoppingCartCreateView);
        Task<ShoppingCartView?> ViewShoppingCart(string userId);

        Task<int> ClearCartAsync(string userId);


        Task<CreateStatus<ShoppingCartCreateView>> IncreaseCartCountByOne(int id);
        Task<CreateStatus<ShoppingCartCreateView>> decresedCartCountByOne(int id);
        Task<CreateStatus<ShoppingCartCreateView>> Remove(int id); 



    }
}
