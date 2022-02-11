using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace WebApiProject.Models.Entities
{
    public class CustomerEntity
    {
        public CustomerEntity(string firstName, string lastName, string email, string telephoneNumber, DateTime dateCreated)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            TelephoneNumber = telephoneNumber;
            DateCreated = dateCreated;
        }
        public CustomerEntity(string firstName, string lastName, string email, string telephoneNumber, DateTime dateCreated, AddressEntity address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            TelephoneNumber = telephoneNumber;
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

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;

        [Required]
        public int AddressId { get; set; }
        public AddressEntity Address { get; set; } 


        [Required, Column(TypeName = "varbinary(max)")]
        public byte[] Hash { get; set; } //Hash name should be changed

        [Required, Column (TypeName = "varbinary(max)")]
        public byte[] Salt { get; set; } //Salt name should be changed

        public ICollection<OrdersEntity> OrdersList { get; set; }

        public void CreateSecurePassword(string password) {
            using (var hmac = new HMACSHA512())
            {
                Salt = hmac.Key;
                Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            }

        }

        public bool CompareSecurePassword(string password)
        {
            using (var hmac = new HMACSHA512(Salt))
            {
                var _hash = hmac.ComputeHash(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                for (int i = 0; i < _hash.Length; i++)
                    if(_hash[i] != _hash[i])
                        return false;

                    return true;
                
            }
        }
    }
}
