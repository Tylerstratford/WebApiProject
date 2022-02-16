using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Models.Entities
{
    public class OrderLinesEntity
    {
        public OrderLinesEntity()
        {

        }

        public OrderLinesEntity(int quantity,ProductEntity product)
        {
            Quantity = quantity;
            Product = product;
        }

        public OrderLinesEntity(int productId, int quantity, decimal linePrice)
        {
            ProductId = productId;
            Quantity = quantity;
            LinePrice = linePrice;
        }

        public OrderLinesEntity(int orderId, int productId, int quantity, decimal linePrice)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            LinePrice = linePrice;
        }


        [Key]
        public int Id { get; set; } 

        [ForeignKey("Order")]
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName ="money")]
        public decimal LinePrice { get; set; }

        public ProductEntity Product { get; set; }
        public OrdersEntity Order { get; set; }
      

    }
}
