using WebApiProject.Models.AddressModels;
using WebApiProject.Models.Entities;

namespace WebApiProject.Models.CustomerModels
{
    public class CustomerModel
    {
        public CustomerModel()
        {

        }
        public CustomerModel(int id, string firstName, string lastName, string email, string telephoneNumber, DateTime createdDate, int addressId, AddressModel addressModel)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            TelephoneNumber = telephoneNumber;
            CreatedDate = createdDate;
            AddressId = addressId;
            AddressModel = addressModel;
        }

        public CustomerModel(int id, string firstName, string lastName, string email, string telephoneNumber, DateTime createdDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            TelephoneNumber = telephoneNumber;
            CreatedDate = createdDate;
        }


        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int AddressId { get; set; }
        public AddressModel AddressModel { get; set; }

    }
}
