using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Data.Models
{
    public class Product : BaseEntity
    {
       public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; }
        public decimal Price { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Discount { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public List<Image> Images { get; set; }
        public List<Review> Reviews { get; set; }
      //  [ConcurrencyCheck]
        public byte[] RowVersion { get; set; }

    }
}
