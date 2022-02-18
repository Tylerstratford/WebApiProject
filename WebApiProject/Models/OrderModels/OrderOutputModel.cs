using WebApiProject.Models.AddressModels;
using WebApiProject.Models.CustomerModels;
using WebApiProject.Models.OrderLinesModels;
using WebApiProject.Models.StatusModels;

namespace WebApiProject.Models.OrderModels
{
    public class OrderOutputModel
    {
        public OrderOutputModel(int id, DateTime created, DateTime updated, CustomerOutputModel customer, AddressModel address, StatusModel orderStatus, ICollection<OrderLinesOutputModel> lines, decimal total)
        {
            Id = id;
            Created = created;
            Updated = updated;
            Customer = customer;
            Address = address;
            OrderStatus = orderStatus;
            Lines = lines;
            Total = total;
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public CustomerOutputModel Customer { get; set; }
        public StatusModel OrderStatus { get; set; }

        public AddressModel Address { get; set; }
        public ICollection<OrderLinesOutputModel> Lines { get; set; }

        public decimal Total { get; set; }
    }
}
