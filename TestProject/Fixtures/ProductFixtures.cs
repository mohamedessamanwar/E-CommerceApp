using BusinessAccessLayer.DTOS.ProductDtos;
using E_CommerceApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Fixtures
{
    public class ProductFixtures
    {
        public static List<ProductWithCategoryDto> GetSampleProducts()
        {
            return new List<ProductWithCategoryDto>()
        {
            new ProductWithCategoryDto()
            {
                Id = 1,
                Name = "Product 1",
                Model = "Model A",
                Price = 100.00M,
                CurrentPrice = 90.00M,
                Discount = 10.00M,
                Description = "Description of Product 1",
                CategoryID = 101,
                CategoryName = "Category A",
                Images = new List<string>() { "image1.jpg", "image2.jpg" }
            },
            new ProductWithCategoryDto()
            {
                Id = 2,
                Name = "Product 2",
                Model = "Model B",
                Price = 200.00M,
                CurrentPrice = 180.00M,
                Discount = 20.00M,
                Description = "Description of Product 2",
                CategoryID = 102,
                CategoryName = "Category B",
                Images = new List<string>() { "image3.jpg", "image4.jpg" }
            }
            // Add more products as needed
        };
        }

    }
}
