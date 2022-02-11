namespace WebApiProject.Models.AddressModels
{
    public class AddressModel
    {
        public AddressModel()
        {
        }

        public AddressModel(string streetName, string postalCode, string city, string country)
        {
            StreetName = streetName;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        public AddressModel(int id, string streetName, string postalCode, string city, string country)
        {
            Id = id;
            StreetName = streetName;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        public int Id { get; set; }
        public string StreetName { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

    }
}
