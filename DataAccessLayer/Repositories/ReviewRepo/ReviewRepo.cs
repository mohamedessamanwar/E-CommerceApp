using DataAccessLayer.Data.Context;
using DataAccessLayer.Data.Models;
using DataAccessLayer.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.ReviewRepo
{
    public class ReviewRepo : GenericRepo<Review> , IReviewRepo
    { 
        public ReviewRepo(ECommerceContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Review>> GetReviewByProduct(int productId)
        {
            var reviews = await context.reviews.Include(p=> p.User)
                .Where(r => r.ProductId == productId)
                .ToListAsync();

            return reviews;
        }


    }
}
