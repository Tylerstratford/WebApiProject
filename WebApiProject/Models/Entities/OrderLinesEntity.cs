using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Models.Entities
{
    public class OrderLinesEntity
    {
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
