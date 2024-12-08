using BusinessAccessLayer.Validations.ProductValidation;
using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.DTOS.ProductDtos;
using BusinessAccessLayer.DTOS.Response;
using BusinessAccessLayer.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BusinessAccessLayer.Services.CacheService;
using E_CommerceApp.Fillter;
using Business_Access_Layer.DTOS.ProductDtos;

namespace E_CommerceApp.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0", Deprecated = true)]
    public class ProductController : BaseController
    {
        private readonly IProductServices productServices;
        private readonly ICacheService cacheService;
        public ProductController(IProductServices productServices, ICacheService cacheService)
        {
            this.productServices = productServices;
            this.cacheService = cacheService;
        }
        [HttpGet]
        public async Task<IActionResult> ProductsWithCategoryV1()
        {
            var cacheKey = $"ProductWithCategoryDto";
            var cachedData = cacheService.GetCache<List<ProductWithCategoryDto>>(cacheKey);
            if (cachedData != null)
            {
                return NewResult(new ResponseHandler().Success(cachedData));
            }
            else
            {
                List<ProductWithCategoryDto> products = await productServices.ProductsWithCategory();
                if (products.Count == 0)
                    return NewResult(new ResponseHandler().NotFound<List<ProductWithCategoryDto>>("Not Found Product"));
                cacheService.SetCache(cacheKey, products, TimeSpan.FromMinutes(5));

                return NewResult(new ResponseHandler().Success<List<ProductWithCategoryDto>>(products));
            }

        }
        #region V2
      //  [HttpGet]
        //public async Task<IActionResult> ProductsWithCategoryV2()
        //{
        //    var cacheKey = $"ProductWithCategoryDto";
        //    var cachedData = cacheService.GetCache<List<ProductWithCategoryDto>>(cacheKey);
        //    if (cachedData != null)
        //    {
        //        return NewResult(new ResponseHandler().Success(cachedData));
        //    }
        //    else
        //    {
        //        List<ProductWithCategoryDto> products = await productServices.ProductsWithCategory();
        //        if (products.Count == 0)
        //            return NewResult(new ResponseHandler().NotFound<List<ProductWithCategoryDto>>("Not Found Product"));
        //        cacheService.SetCache(cacheKey, products, TimeSpan.FromMinutes(5));

        //        return NewResult(new ResponseHandler().Success<List<ProductWithCategoryDto>>(products));

        //    }


        //}
        #endregion       
        #region ProductWithPagination
        [HttpGet("ProductWithPagination/V2")]
        public async Task<IActionResult> ProductWithPaginationv2([FromQuery] int pageNumber, [FromQuery] int orderBy, [FromQuery] int fees, [FromQuery] int CategoryId, [FromQuery] string? Search)
        {
            var products = await productServices.GetProductsWithFillter(pageNumber, orderBy, fees, CategoryId, Search);
            if (products.Count == 0)
                return NewResult(new ResponseHandler().NotFound<ProductViewPagination>("Not Found Product"));
            return NewResult(new ResponseHandler().Success(products));
        }
        #endregion
        #region Create
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto createProductDto)
        {
            var validator = new CreateProductDtoValidation();
            var validationResult = await validator.ValidateAsync(createProductDto);
            if (!validationResult.IsValid)
            {
                var Errors = validator.ListError(validationResult);
                return NewResult(new ResponseHandler().BadRequest<CreateProductDto>(Errors));
            }
            ViewProduct viewProduct = await productServices.CreateProduct(createProductDto);
            return NewResult(new ResponseHandler().Success(viewProduct));

        }
        #endregion
        #region Id
        [HttpGet]
        [Route(("{Id}"))]
        public async Task<IActionResult> GetProduct(int Id)
        {
            var products = await productServices.ProductWithCategory(Id);
            if (products == null)
                return NewResult(new ResponseHandler().NotFound<ProductWithCategoryDtoProcudere>("Not Found Product"));
            return NewResult(new ResponseHandler().Success(products));
        }
        #endregion
    }
}
