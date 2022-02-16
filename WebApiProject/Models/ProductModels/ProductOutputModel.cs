namespace WebApiProject.Models.ProductModels
{
    public class ProductOutputModel
    {
        public ProductOutputModel(int id, string productName, string articleNumber, decimal price)
        {
            Id = id;
            ProductName = productName;
            ArticleNumber = articleNumber;
            Price = price;
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ArticleNumber { get; set; }

        public decimal Price { get; set; }


    }
}
