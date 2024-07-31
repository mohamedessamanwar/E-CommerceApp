using BusinessAccessLayer.Validations.ProductValidation;
using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.DTOS.ProductDtos;
using BusinessAccessLayer.DTOS.Response;
using BusinessAccessLayer.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ProductController : BaseController
    {
        private readonly IProductServices productServices;
        public ProductController(IProductServices productServices)
        {
            this.productServices = productServices;
        }
        [HttpGet]
        public async Task<IActionResult> ProductsWithCategory()
        {
          //  productServices.ProductsWithCategory();
            List<ProductWithCategoryDto> products = await productServices.ProductsWithCategory();
            if (products.Count == 0)
                return NewResult(new ResponseHandler().NotFound<List<ProductWithCategoryDto>>("Not Found Product"));
            return NewResult(new ResponseHandler().Success(products));
        }
        [HttpGet("ProductWithPagination")]
        public async Task<IActionResult> ProductWithPagination([FromQuery] Pagination pagination)
        {
            var products = await productServices.GetProductWithPagination(pagination);
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
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ProductWithCategory(int Id)
        {
            var products = await productServices.ProductWithCategory(Id);
            if (products == null)
                return NewResult(new ResponseHandler().NotFound<ProductWithCategoryDtoProcudere>("Not Found Product"));
            return NewResult(new ResponseHandler().Success(products));
        }
    }
}
