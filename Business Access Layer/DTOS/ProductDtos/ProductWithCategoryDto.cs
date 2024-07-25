namespace BusinessAccessLayer.DTOS.ProductDtos
{
    public class ProductWithCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; }
        public decimal Price { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public List<string>? Images { get; set; }
    }
}

