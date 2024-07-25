using DataAccessLayer.Data.Models;
using DataAccessLayer.Repositories.GenericRepo;

namespace DataAccessLayer.Repositories.ProductRepo
{
    public interface IProductRepository : IGenericRepo<Product>
    {
        Task<IEnumerable<Product>> GetProductsWithCategories();
        Task<IEnumerable<ProductWithCategory>> GetProductWithCategory(int Id);
        Task<IEnumerable<Product>> GetProductsWithPagination(int PageNumber, int PageSize, string[]? OrderBy, string? Search);
         Task<Product?> GitId(int Id); 
    }
}
