using WebApiProject.Models.CustomerModels;
using WebApiProject.Models.OrderLinesModels;
using WebApiProject.Models.StatusModels;

namespace WebApiProject.Models.OrderModels
{
    public class OrderCreateModel
    {
        private int customerId;
        private List<OrderLinesCreateModel> lines;


        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public List<OrderLinesCreateModel> Lines
        {
            get { return lines; }
            set
            {
                var _lines = new List<OrderLinesCreateModel>();
                foreach(var i in value)
                {
                    _lines.Add(new OrderLinesCreateModel() {ProductId = i.ProductId, Quantity = i.Quantity});
                }

                lines = _lines;
            }
            
        }
        public int StatusId { get; private set; } = 1;
       

    }
}
