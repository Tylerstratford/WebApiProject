using WebApiProject.Models.ProductModels;

namespace WebApiProject.Models.OrderLinesModels
{
    public class OrderLinesOutputModel
    {
        public OrderLinesOutputModel(int quantity, decimal linePrice, ProductOutputModel product)
        {
            Quantity = quantity;
            LinePrice = linePrice;
            Product = product;
        }

        public OrderLinesOutputModel(int id, int orderId, int productId, int quantity, decimal linePrice, ProductOutputModel product)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            LinePrice = linePrice;
            Product = product;
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }  
        public int Quantity { get; set; }
        public decimal LinePrice { get; set; }  
        public ProductOutputModel Product { get; set; }

    }
    
}


