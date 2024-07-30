using DataAccessLayer.Data.Context;
using DataAccessLayer.Data.Models;
using DataAccessLayer.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.OrderRepo
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        public OrderRepo(ECommerceContext context) : base(context)
        {
        }
    }
}
