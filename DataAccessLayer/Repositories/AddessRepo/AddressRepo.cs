using DataAccessLayer.Data.Context;
using DataAccessLayer.Data.Models;
using DataAccessLayer.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.AddessRepo
{
    public class AddressRepo : GenericRepo<Address>, IAddressRepo
    {
      //  private readonly ECommerceContext _context;
        public AddressRepo(ECommerceContext context) : base(context)
        {
           // _context = context;
        }

        public async Task<List<Address>> GetAddressByUserId(string userId)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Thread ID before awaiting: {threadId}");

            var addresses = await context.addresses
                                          .AsNoTracking()
                                          .Where(a => a.UserId == userId)
                                          .ToListAsync();

            threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Thread ID after awaiting: {threadId}");
            return addresses;
        }

    }
}
