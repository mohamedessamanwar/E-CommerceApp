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

namespace E_CommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductController : BaseController
    {
   //     private readonly FillterAction _filter;
        private readonly IProductServices productServices;
        private readonly ICacheService cacheService;
        public ProductController(IProductServices productServices, ICacheService cacheService)
        {
            this.productServices = productServices;
            this.cacheService = cacheService;
        }
        [HttpGet]
        [ServiceFilter(typeof(FillterAction))] // Apply the filter to a specific action
        public async Task<IActionResult> ProductsWithCategory()
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
        [HttpGet("ProductWithPagination")]
        public async Task<IActionResult> ProductWithPagination([FromQuery] Pagination pagination)
        {
            var products = await productServices.GetProductWithPagination(pagination);
            if (products.Count == 0)
                return NewResult(new ResponseHandler().NotFound<List<ViewProduct>>("Not Found Product"));
            return NewResult(new ResponseHandler().Success(products));
        }
        [HttpGet("ProductWithPagination/V2")]
        public async Task<IActionResult> ProductWithPaginationv2([FromQuery]int pageNum = 1, [FromQuery] int orderBy = 0, [FromQuery] string category = null, [FromQuery] int fees = 0)
        {
            var products = await productServices.GetProductWithPaginationV2(pageNum, orderBy, category, fees);
            if (products.Count == 0)
                return NewResult(new ResponseHandler().NotFound<List<ViewProduct>>("Not Found Product"));
            return NewResult(new ResponseHandler().Success(products));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm]CreateProductDto createProductDto)
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

        [HttpGet]
       
        [Route(("GetProduct/{Id}"))]
        [ServiceFilter(typeof(FillterAction))] // Apply the filter to a specific action
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetProduct(int Id)
        {
            var products = await productServices.ProductWithCategory(Id);
            if (products == null)
                return NewResult(new ResponseHandler().NotFound<ProductWithCategoryDtoProcudere>("Not Found Product"));
            return NewResult(new ResponseHandler().Success(products));
        }
    }
}
