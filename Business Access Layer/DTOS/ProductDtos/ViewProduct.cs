using System.Text.Json.Serialization;

namespace BusinessAccessLayer.DTOS.ProductDtos
{
    public class ViewProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public int SaveNum { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
