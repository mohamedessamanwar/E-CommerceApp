using DataAccessLayer.Data.Context;
using DataAccessLayer.Data.Models;
using DataAccessLayer.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.ShoppingCartRepo
{
    public class ShoppingCartRepo : GenericRepo<ShoppingCart> , IShoppiingCartRepo
    {
        public ShoppingCartRepo(ECommerceContext context) : base(context)
        {
            
        }
        public async Task<IEnumerable<ShoppingCart>> GetShoppingCartsByUserCart(string userId)
        {
           return  await context.shoppingCarts.Where(s=> s.UserId == userId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ShoppingCart>> GetShoppingWithProductView(string userId)
        {
            return await context.shoppingCarts.Include(s => s.Product).ThenInclude(p=> p.Images).Where(s => s.UserId == userId).AsNoTracking().ToListAsync();
        }

        public  ShoppingCart getid(string userId)
        {
            return  context.shoppingCarts.Where(s => s.UserId == userId).FirstOrDefault();
        }



    }
}
