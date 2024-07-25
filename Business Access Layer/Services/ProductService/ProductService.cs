using AutoMapper;
using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.DTOS.ProductDtos;
using DataAccessLayer.Data.Models;
using DataAccessLayer.UnitOfWorkRepo;
using Microsoft.AspNetCore.Http;

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
        public  async Task<List<ProductWithCategoryDto>> ProductsWithCategory()
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
            if (createProductDto.Cover != null)
            {
                var images = await SaveImages(createProductDto.Cover);
                product.Images = images.Select(i => new Image
                {
                    Url = i
                }).ToList();

            }
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
            if (product != null) { return _mapper.Map<ProductWithCategoryDtoProcudere>(product); }
            return null; 
        }
        private async Task<List<string>> SaveImages(List<IFormFile> files)
        {

            List<string> images = new List<string>();
            foreach (var file in files) {
                var coverName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var path = Path.Combine(Helper.Helper.ImagesPath, coverName);
                using var stream = File.Create(path);
                await file.CopyToAsync(stream);
                coverName = $"{Helper.Helper.URL}/{coverName}"; 
                images.Add(coverName);

            }
            return images;
        }


    }
}
