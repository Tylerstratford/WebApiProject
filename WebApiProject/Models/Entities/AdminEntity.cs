using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace WebApiProject.Models.Entities
{
    public class AdminEntity
    {
        public AdminEntity()
        {

        }

        public AdminEntity(string name, string lastName, string email)
        {
            Name = name;
            LastName = lastName;
            Email = email;
        }

        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Required, Column(TypeName = "varbinary(max)")]
        public byte[] Hash { get; set; } //Hash name should be changed

        [Required, Column(TypeName = "varbinary(max)")]
        public byte[] Salt { get; set; } //Salt name should be changed

        public void CreateSecurePassword(string password)
        {
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
                var _hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < _hash.Length; i++)
                    if (_hash[i] != Hash[i])
                        return false;

                return true;

            }
        }
    }
}
