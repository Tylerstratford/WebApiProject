using WebApiProject.Models.Entities;
using WebApiProject.Models.ProductModels;

namespace WebApiProject.Models.OrderLinesModels
{
    public class OrderLinesCreateModel
    {
        public OrderLinesCreateModel()
        {
        }

        public OrderLinesCreateModel(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
