using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Models.Entities
{
    public class OrderStatusEntity
    {
        public OrderStatusEntity()
        {

        }
        public OrderStatusEntity(string status)
        {
            Status = status;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Status { get; set; }

        public ICollection<OrdersEntity> Orders { get; set; }
    }
}
