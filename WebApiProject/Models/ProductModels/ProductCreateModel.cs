namespace WebApiProject.Models.ProductModels
{
    public class ProductCreateModel
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CategoryName { get; set; }
    }
}
