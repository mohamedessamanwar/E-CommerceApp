using DataAccessLayer.Data.Context;
using DataAccessLayer.Data.Models;
using DataAccessLayer.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.OrderItemRepo
{
    public class OrderItemRepo : GenericRepo<OrderDetail>, IOrderItemRepo
    {
        public OrderItemRepo(ECommerceContext context) : base(context)
        {
        }
    }
}
