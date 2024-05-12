namespace BusinessAccessLayer.DTOS.ProductDtos
{
    public class CreateProductDto
    {
        public string? Name { get; set; } = string.Empty;
        public string? Model { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public string? Description { get; set; } = string.Empty;
        public int? CategoryID { get; set; }
    }
}
