namespace WebApiProject.Models.ProductModels
{
    public class ProductUpdateModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime Updated { get; set; }
        public string CategoryName { get; set; }
    }
}
