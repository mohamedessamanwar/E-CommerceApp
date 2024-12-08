using Dapper;
using DataAccessLayer.Data.Context;
using DataAccessLayer.Data.Models;
using DataAccessLayer.Repositories.GenericRepo;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccessLayer.Repositories.ProductRepo
{
    public class ProductRepository : GenericRepo<Product>, IProductRepository
    {
        private readonly ECommerceContext _context;
        public readonly DapperContext _dapperContext;
        public ProductRepository(ECommerceContext context, DapperContext dapperContext) : base(context)
        {
            _context = context;
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<Product>> GetProductsWithCategories()
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                string sql = "select * from Products join categories on  Products.CategoryID = categories.Id";
                return await connection.QueryAsync<Product>(sql);
            }
        }
        public async Task<IEnumerable<Product>> GetProductsWithPagination(int PageNumber, int PageSize ,string[]? OrderBy, string? Search)
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
        public async Task<Product?> GetProductWithLock(int Id)
        {
            return await _context.Products
               .FromSqlRaw("SELECT * FROM Products WITH (UPDLOCK ,ROWLOCK) WHERE Id = {0}", Id).AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsWithFillter(int pageNum ,int orderBy , int fees  , int CategoryId,  string Search)
        {
            var products = context.Products.Include(p=> p.Category).Include(p=> p.Images)
                .Include(p=> p.Reviews).AsNoTracking();

            // Filtering based on search keyword
            if (!string.IsNullOrEmpty(Search))
            {
                products = products.Where(p => p.Name.Contains(Search) || p.Description.Contains(Search));
            }
            if (CategoryId != 0)
            {
                products = products.Where(p => p.CategoryID == CategoryId);
            }
            if (fees > 0)
            {
                switch (fees)
                {
                    case 1:
                        products = products.Where(p => p.CurrentPrice > 0 && p.CurrentPrice < 9999);
                        break;

                    case 2:
                        products = products.Where(p => p.CurrentPrice >= 10000 && p.CurrentPrice < 20000);
                        break;

                    case 3:
                        products = products.Where(p => p.CurrentPrice >= 20000 && p.CurrentPrice < 30000);
                        break;

                    case 4:
                        products = products.Where(p => p.CurrentPrice >= 30000 && p.CurrentPrice < 50000);
                        break;

                    case 5:
                        products = products.Where(p => p.CurrentPrice >= 50000);
                        break;

                    default:
                        // Optionally, you can handle cases where the fee doesn't match any category.
                        break;
                }
            }

            if (orderBy > 0)
            {
                products = orderBy switch
                {
                    1 => products.OrderBy(p => p.CurrentPrice),
                    2 => products.OrderByDescending(p => p.Reviews.Any() ? p.Reviews.Average(a => a.Rate) : 0),
                };
            }
             products =products.Skip((pageNum - 1) * 10).Take(10);
            return await products.ToListAsync();
        }

        public async Task<int> GetProductsWithFillterCount(int pageNum, int orderBy, int fees, int CategoryId, string Search)
        {
            var products = context.Products.Include(p => p.Category).Include(p => p.Images)
                .Include(p => p.Reviews).AsNoTracking();

            // Filtering based on search keyword
            if (!string.IsNullOrEmpty(Search))
            {
                products = products.Where(p => p.Name.Contains(Search) || p.Description.Contains(Search));
            }
            if (CategoryId != 0)
            {
                products = products.Where(p => p.CategoryID == CategoryId);
            }
            if (fees > 0)
            {
                switch (fees)
                {
                    case 1:
                        products = products.Where(p => p.CurrentPrice > 0 && p.CurrentPrice < 9999);
                        break;

                    case 2:
                        products = products.Where(p => p.CurrentPrice >= 10000 && p.CurrentPrice < 20000);
                        break;

                    case 3:
                        products = products.Where(p => p.CurrentPrice >= 20000 && p.CurrentPrice < 30000);
                        break;

                    case 4:
                        products = products.Where(p => p.CurrentPrice >= 30000 && p.CurrentPrice < 50000);
                        break;

                    case 5:
                        products = products.Where(p => p.CurrentPrice >= 50000);
                        break;

                    default:
                        // Optionally, you can handle cases where the fee doesn't match any category.
                        break;
                }
            }

            if (orderBy > 0)
            {
                products = orderBy switch
                {
                    1 => products.OrderBy(p => p.CurrentPrice),
                    2 => products.OrderByDescending(p => p.Reviews.Any() ? p.Reviews.Average(a => a.Rate) : 0),
                };
            }
            int  productCount = await products.CountAsync();
            return productCount;
        }

    }
}
