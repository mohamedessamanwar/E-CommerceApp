using DataAccessLayer.Data.Context;
using DataAccessLayer.Data.Models;
using DataAccessLayer.Repositories.GenericRepo;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.ProductRepo
{
    public class ProductRepository : GenericRepo<Product>, IProductRepository
    {
        private readonly ECommerceContext _context;
        public ProductRepository(ECommerceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsWithCategories()
        {
            return await _context.Products.AsNoTracking().Include(p => p.Category).Include(p=> p.Images).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsWithPagination(int PageNumber, int PageSize, string[]? OrderBy, string? Search)
        {
            IQueryable<Product> queryable = _context.Products.AsNoTracking();

            // Filtering based on search keyword
            if (!string.IsNullOrEmpty(Search))
            {
                queryable = queryable.Where(p => p.Name.Contains(Search) || p.Description.Contains(Search));
            }

            // Sorting based on OrderBy parameter
            if (OrderBy != null && OrderBy.Any())
            {
                foreach (var orderByField in OrderBy)
                {
                    switch (orderByField)
                    {
                        case "Name":
                            queryable = queryable.OrderBy(p => p.Name);
                            break;
                        case "Price":
                            queryable = queryable.OrderBy(p => p.Price);
                            break;
                            // Add more cases for other fields as needed
                    }
                }
            }
            else
            {
                // Default sorting if no OrderBy parameter is provided
                queryable = queryable.OrderBy(p => p.Id);
            }

            // Pagination
            queryable = queryable.Skip((PageNumber - 1) * PageSize).Take(PageSize);

            // Execute the query and return the results
            return await queryable.ToListAsync();
        }
        public async Task<IEnumerable<ProductWithCategory>> GetProductWithCategory(int Id)
        {
            var Idparam = new SqlParameter("id", Id);
            return  await _context.ProductWithCategories.FromSqlRaw($"ProductWithCategory @id", Idparam).ToListAsync();

        }
        public async Task<Product?> GitId(int Id)
        {
            return await _context.Products
               .FromSqlRaw("SELECT * FROM Products WITH (UPDLOCK, ROWLOCK) WHERE Id = {0}", Id)
                .FirstOrDefaultAsync();
        }

    }
}
