using BusinessAccessLayer.DTOS.ShoppingCart;
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
    }
}
