namespace BusinessAccessLayer.DTOS.ProductDtos
{
    public class ProductWithCategoryDtoProcudere
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
