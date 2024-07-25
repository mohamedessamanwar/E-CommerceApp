using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.DTOS.ProductDtos;

namespace BusinessAccessLayer.Services.ProductService
{
    public interface IProductServices
    {
       Task< List<ProductWithCategoryDto>> ProductsWithCategory();
        Task<ViewProduct> CreateProduct(CreateProductDto createProductDto);
        Task<List<ViewProduct>> GetProductWithPagination(Pagination pagination);
        Task<ProductWithCategoryDtoProcudere> ProductWithCategory(int Id);
    }
}
