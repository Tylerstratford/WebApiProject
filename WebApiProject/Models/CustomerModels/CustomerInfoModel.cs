using WebApiProject.Models.AddressModels;

namespace WebApiProject.Models.CustomerModels
{
    public class CustomerInfoModel
    {
        public CustomerInfoModel(int id, string firstName, string lastName, string email, string telephoneNumber, DateTime createdDate, DateTime updatedDate, int addressId, AddressModel addressModel)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            TelephoneNumber = telephoneNumber;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            AddressId = addressId;
            AddressModel = addressModel;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set;} 
        public int AddressId { get; set; }
        public AddressModel AddressModel { get; set; }
    }
}
