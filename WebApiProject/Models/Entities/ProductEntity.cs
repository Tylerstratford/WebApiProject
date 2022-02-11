using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Models.Entities
{
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string ProductName { get; set; }

        public string ArticleNumber { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Description { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        [Required]
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }

    }
}
