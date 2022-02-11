using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProject.Models.Entities
{
    public class CustomerEntity
    {
        public CustomerEntity(string firstName, string lastName, string email, string telephoneNumber, string password, DateTime dateCreated)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            TelephoneNumber = telephoneNumber;
            Password = password;
            DateCreated = dateCreated;
        }
        public CustomerEntity(string firstName, string lastName, string email, string telephoneNumber, string password, DateTime dateCreated, AddressEntity address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            TelephoneNumber = telephoneNumber;
            Password = password;
            DateCreated = dateCreated;
            Address = address;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; } 

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; } 

        [Required]
        [Column(TypeName = "char(20)")]
        public string TelephoneNumber { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; } 

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;

        [Required]
        public int AddressId { get; set; }
        public AddressEntity Address { get; set; } 

        public ICollection<OrdersEntity> OrdersList { get; set; }


    }
}
