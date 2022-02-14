using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Models.Entities
{
    public class CategoryEntity
    {
        public CategoryEntity()
        {

        }
        public CategoryEntity(string categoryName)
        {
            CategoryName = categoryName;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string CategoryName { get; set; }

        public ICollection<ProductEntity> Products { get; set; } = null!;

    }
}
