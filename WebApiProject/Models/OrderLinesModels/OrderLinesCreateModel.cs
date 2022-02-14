using WebApiProject.Models.Entities;

namespace WebApiProject.Models.OrderLinesModels
{
    public class OrderLinesCreateModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal LinePrice => Quantity * Product.Price;
        public ProductEntity Product { get; set; }
        public OrdersEntity Order { get; set; }
    }
}
