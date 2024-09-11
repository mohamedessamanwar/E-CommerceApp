namespace DataAccessLayer.Data.Models
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Product> Products { get; set; } = new HashSet<Product>();
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
    }
}
