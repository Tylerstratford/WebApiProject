namespace WebApiProject.Models.ProductModels
{
    public class ProductCreateModel
    {
        private string productName { get; set; }
        private string categoryName { get; set; }

        public string ProductName
        {
            get { return productName; }
            set { productName = value.Trim(); }
        }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value.Trim(); }
        }
    }
}
