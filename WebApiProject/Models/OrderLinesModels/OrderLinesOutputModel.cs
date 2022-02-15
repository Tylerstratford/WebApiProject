using WebApiProject.Models.ProductModels;

namespace WebApiProject.Models.OrderLinesModels
{
    public class OrderLinesOutputModel
    {
        public OrderLinesOutputModel(int quantity, decimal linePrice, ProductModel product)
        {
            Quantity = quantity;
            LinePrice = linePrice;
            Product = product;
        }

        public int Quantity { get; set; }
        public decimal LinePrice { get; set; }
        public ProductModel Product { get; set; }
    }
}
