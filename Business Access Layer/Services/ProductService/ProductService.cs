using AutoMapper;
using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.DTOS.ProductDtos;
using DataAccessLayer.Data.Models;
using DataAccessLayer.UnitOfWorkRepo;

namespace BusinessAccessLayer.Services.ProductService
{
    public class ProductService : IProductServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ProductWithCategoryDto>> ProductsWithCategory()
        {
            List<Product> products = (await unitOfWork.productRepository.GetProductsWithCategories()).ToList();
            if (products == null)
            {
                return new List<ProductWithCategoryDto>();
            }
            var result = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return result;
        }
        public async Task<ViewProduct> CreateProduct(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            await unitOfWork.productRepository.AddAsync(product);
            int SaveNum = unitOfWork.Complete();
            var result = _mapper.Map<ViewProduct>(product);
            result.SaveNum = SaveNum;
            return result;
        }
        public async Task<List<ViewProduct>> GetProductWithPagination(Pagination pagination)
        {
            var products = await unitOfWork.productRepository.GetProductsWithPagination(pagination.PageNumber, pagination.PageSize, pagination.OrderBy, pagination.Search);
            var productResult = _mapper.Map<List<ViewProduct>>(products);
            return productResult;
        }
        public async Task<ProductWithCategoryDtoProcudere> ProductWithCategory(int Id)
        {
            var product = (await unitOfWork.productRepository.GetProductWithCategory(Id)).FirstOrDefault();
            return _mapper.Map<ProductWithCategoryDtoProcudere>(product);

        }


    }
}
