using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Models.Entities
{
    public class OrdersEntity
    {
      
        public OrdersEntity()
        {

        }

        public OrdersEntity(CustomerEntity customer, OrderStatusEntity orderStatus, decimal total, ICollection<OrderLinesEntity> lines)
        {
            Customer = customer;
            OrderStatus = orderStatus;
            Total = total;
            Lines = lines;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        public CustomerEntity Customer { get; set; }

        public int OrderStatusId { get; set; }

        public OrderStatusEntity OrderStatus { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        public ICollection<OrderLinesEntity> Lines { get; set; }
    }
}
