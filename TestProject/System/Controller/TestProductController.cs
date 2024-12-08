//using BusinessAccessLayer.DTOS.ProductDtos;
//using BusinessAccessLayer.DTOS.Response;
//using BusinessAccessLayer.Services.ProductService;
//using DataAccessLayer.Data.Models;
//using E_CommerceApp.Controllers;
//using FluentAssertions;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using TestProject.Fixtures;

//namespace TestProject.System.Controller
//{

//    public class TestProductController 
//    {
//        [Fact]
//        public async Task ProductsWithCategory_OnSuccess_ReturnProducts()
//        {
//            //A mock object is a simulated version of a real object that you can use to test your code in
//            //isolation. It allows you to define how the mock should behave
//            //when certain methods are called, without relying on actual implementations.
//            // 1- Unit Testing: Moq allows you to isolate the code you're testing
//            // from its dependencies by creating mock objects. 
//            // 2- Control: You can control how the mock objects behave, making it easier to test different scenarios
//            // (e.g., how your code handles exceptions, null values, or specific return types).
//            // 3 - No Need for Real Services: Moq enables you to test your code without needing access to actual databases,
//            // web services, or other external dependencies.
//            // This makes your tests faster and more reliable.
//            var mockService = new Mock<IProductServices>();
//            // This line configures the mock object to respond in a specific way when the
//            // ProductsWithCategory method is called.
//            //Setup(s => s.ProductsWithCategory()) indicates that when the ProductsWithCategory
//            //method is called on the mock IProductServices object, it should perform the action
//            //specified in ReturnsAsync.
//            mockService.Setup(s => s.ProductsWithCategory()).ReturnsAsync(ProductFixtures.GetSampleProducts());
//            var productController = new ProductController(mockService.Object);
//            var result = await productController.ProductsWithCategory();
//            // Cast the result to OkObjectResult
//            var okResult = result as ObjectResult;
//            // Assert the result type
//            okResult.Should().NotBeNull();
//            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
//            // Assert the value type
//            okResult.Value.Should().BeOfType<Response<List<ProductWithCategoryDto>>>();
//        }
//        [Fact]
//        public async Task ProductsWithCategory_OnNotFound_NotFound()
//        {
//            var mockService = new Mock<IProductServices>();
//            mockService.Setup(s => s.ProductsWithCategory()).ReturnsAsync(new List<ProductWithCategoryDto>());
//            var productController = new ProductController(mockService.Object);
//            var result = await productController.ProductsWithCategory();
//            // Assert
//            var okResult = result as ObjectResult;
//            okResult.Should().NotBeNull();
//            okResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
//            okResult.Value.Should().BeOfType<Response<List<ProductWithCategoryDto>>>();
//            var response = okResult.Value as Response<List<ProductWithCategoryDto>>;
//            response.Message.Should().Be("Not Found Product");
//        }


//        [Fact]
//        public async Task GetProduct_OnSuccess_ReturnProduct()
//        {
//            var mockService = new Mock<IProductServices>();
//            var productWithCategory = new ProductWithCategoryDtoProcudere();
//            mockService.Setup(s => s.ProductWithCategory(1)).ReturnsAsync(productWithCategory);
//            var productController = new ProductController(mockService.Object);
//            var result = await productController.GetProduct(1);
//            // Assert
//            var okResult = result as ObjectResult;
//            okResult.Should().NotBeNull();
//            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
//            okResult.Value.Should().BeOfType<Response<ProductWi thCategoryDtoProcudere>>();
//            var response = okResult.Value as Response<ProductWithCategoryDtoProcudere>;
//            response.Should().NotBeNull();
//            response.Data.Should().Be(productWithCategory);
//        }
//        [Fact]
//        public async Task GetProduct_OnFailer_ReturnNotFound()
//        {
//            var mockService = new Mock<IProductServices>();
//          //  var productWithCategory = new ProductWithCategoryDtoProcudere();
//            mockService.Setup(s => s.ProductWithCategory(100)).ReturnsAsync((ProductWithCategoryDtoProcudere)null);
//            var productController = new ProductController(mockService.Object);
//            var result = await productController.GetProduct(100);
//            // Assert
//            var okResult = result as ObjectResult;
//            okResult.Should().NotBeNull();
//            okResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
//            okResult.Value.Should().BeOfType<Response<ProductWithCategoryDtoProcudere>>();
//            var response = okResult.Value as Response<ProductWithCategoryDtoProcudere>;
//            response.Should().NotBeNull();
//            response.Message.Should().Be("Not Found Product");
//        }

//    }
//}
