using WebApiProject.Models.CustomerModels;
using WebApiProject.Models.Entities;
using WebApiProject.Models.OrderLinesModels;
using WebApiProject.Models.StatusModels;

namespace WebApiProject.Models.OrdeModels
{
    public class OrderModel
    {
        public OrderModel(int id, CustomerModel customer, StatusModel orderStatus, ICollection<OrderLinesModel> lines)
        {
            Id = id;
            Customer = customer;
            OrderStatus = orderStatus;
            Lines = lines;
        }

        public int Id { get; set; }

        public CustomerModel Customer { get; set; }
        public StatusModel OrderStatus { get; set; }
        public ICollection<OrderLinesModel> Lines { get; set; }
    }
}
