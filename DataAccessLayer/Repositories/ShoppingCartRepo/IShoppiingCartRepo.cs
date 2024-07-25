using DataAccessLayer.Data.Models;
using DataAccessLayer.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.ShoppingCartRepo
{
    public interface IShoppiingCartRepo : IGenericRepo<ShoppingCart>
    {
        Task<IEnumerable<ShoppingCart>> GetShoppingCartsByUserCart(string userId);
        Task<IEnumerable<ShoppingCart>> GetShoppingWithProductView(string userId);
        ShoppingCart getid(string userId); 
    }
}
