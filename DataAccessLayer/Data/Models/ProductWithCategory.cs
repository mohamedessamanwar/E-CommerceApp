namespace DataAccessLayer.Data.Models
{
    public class ProductWithCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }


    }
}
