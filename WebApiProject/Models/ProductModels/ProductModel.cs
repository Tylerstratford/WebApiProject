using WebApiProject.Models.CategoryModels;
using WebApiProject.Models.Entities;

namespace WebApiProject.Models.ProductModels
{
    public class ProductModel
    {
        public ProductModel()
        {
        }

        public ProductModel(int id, string productName, string articleNumber, decimal price, string description, DateTime created, DateTime updated, int categoryId, CategoryModel category)
        {
            Id = id;
            ProductName = productName;
            ArticleNumber = articleNumber;
            Price = price;
            Description = description;
            Created = created;
            Updated = updated;
            CategoryId = categoryId;
            Category = category;
        }
        public ProductModel(int id, string productName, decimal price, string description, DateTime created, int categoryId, CategoryModel category)
        {
            Id = id;
            ProductName = productName;
            Price = price;
            Description = description;
            Created = created;
            CategoryId = categoryId;
            Category = category;
        }

        public ProductModel(int id, string productName, string articleNumber, decimal price, string description)
        {
            Id = id;
            ProductName = productName;
            ArticleNumber = articleNumber;
            Price = price;
            Description = description;
        }

        public ProductModel(string productName, string articleNumber, decimal price)
        {
            ProductName = productName;
            ArticleNumber = articleNumber;
            Price = price;

        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ArticleNumber { get; set; }

        public decimal Price { get; set; }
        public string Description { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int CategoryId { get; set; }

        public CategoryModel Category { get; set; }
    }
}
