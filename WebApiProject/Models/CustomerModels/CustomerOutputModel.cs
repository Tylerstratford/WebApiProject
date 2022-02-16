using WebApiProject.Models.AddressModels;

namespace WebApiProject.Models.CustomerModels
{
    public class CustomerOutputModel
    {
        public CustomerOutputModel(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get; set; }
    }
}
