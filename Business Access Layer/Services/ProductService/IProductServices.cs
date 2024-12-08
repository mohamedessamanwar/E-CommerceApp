using Business_Access_Layer.DTOS.ProductDtos;
using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.DTOS.ProductDtos;

namespace BusinessAccessLayer.Services.ProductService
{
    public interface IProductServices
    {
       Task< List<ProductWithCategoryDto>> ProductsWithCategory();
        Task<ViewProduct> CreateProduct(CreateProductDto createProductDto);
       // Task<List<ViewProduct>> GetProductWithPagination(Pagination pagination);
        Task<ProductWithCategoryDtoProcudere> ProductWithCategory(int Id);
        //   Task<List<ViewProduct>> GetProductWithPaginationV2(int pageNum = 1, int orderBy = 0, string category = null, int fees = 0);
        Task<ProductWithPageination> GetProductsWithFillter(int pageNum, int orderBy, int fees, int CategoryId, string Search);
    }
}
