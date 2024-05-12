namespace DataAccessLayer.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; }
        public decimal Price { get; set; }

        public decimal CurrentPrice { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }

    }
}
