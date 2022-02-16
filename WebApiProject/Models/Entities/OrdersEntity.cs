using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Models.Entities
{
    public class OrdersEntity
    {
      
        public OrdersEntity()
        {

        }

        public OrdersEntity(CustomerEntity customer, OrderStatusEntity orderStatus, ICollection<OrderLinesEntity> lines)
        {
            Customer = customer;
            OrderStatus = orderStatus;
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
        

        //Added in latest migration
        public ICollection<OrderLinesEntity> Lines { get; set; }
    }
}
