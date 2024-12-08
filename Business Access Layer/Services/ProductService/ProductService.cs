using AutoMapper;
using Business_Access_Layer.DTOS.ProductDtos;
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
        public async Task<ProductWithPageination> GetProductsWithFillter(int pageNum, int orderBy, int fees, int CategoryId, string Search)
        {
            var products = await unitOfWork.productRepository.GetProductsWithFillter( pageNum,  orderBy,  fees,  CategoryId, Search);
            var count = await unitOfWork.productRepository.GetProductsWithFillterCount(pageNum, orderBy, fees, CategoryId, Search);
            return new ProductWithPageination()
            {
                Count = count,
                Products = products.Select(p => new ProductViewPagination()
                {
                    Name = p.Name,
                    Description = p.Description,
                    Model = p.Model,
                    ImageUrl = p.Images.Select(i => i.Url).ToList() , 
                    CategoryName = p.Category.Name,
                    Id = p.Id , 
                    Discount = p.Discount ,
                    CurrentPrice = p.CurrentPrice ,
                    Price = p.Price ,
                    count = p.Count ,

                }).ToList()
            }; 
        }



    }
}
