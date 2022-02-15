using WebApiProject.Models.Entities;
using WebApiProject.Models.OrderModels;
using WebApiProject.Models.ProductModels;

namespace WebApiProject.Models.OrderLinesModels
{
    public class OrderLinesModel
    {
        public OrderLinesModel()
        {
        }

        public OrderLinesModel(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public OrderLinesModel(int quantity, decimal linePrice, ProductModel product)
        {
         
            Quantity = quantity;
            LinePrice = linePrice;
            Product = product;
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal LinePrice { get; set; }
        //{
        //    get { return LinePrice; }
        //    set { LinePrice = Product.Price * Quantity; }
        //}
        
        public ProductModel Product { get; set; }
        public OrderModel Order { get; set; }

    }

    //public double Amount(decimal price)
    //{
    //    decimal LinePrice = LinePrice * price;
    //    return (double)LinePrice;
    //}
}
