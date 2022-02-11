using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Models.Entities
{
    public class OrdersEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public CustomerEntity Customer { get; set; }
        public OrderStatusEntity OrderStatus { get; set; }
    }
}
