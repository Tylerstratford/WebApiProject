using WebApiProject.Models.StatusModels;

namespace WebApiProject.Models.OrderModels
{
    public class OrderUpdateModel
    {
        public OrderUpdateModel()
        {
        }

        public OrderUpdateModel(int statusId)
        {
            StatusId = statusId;
        }

        public OrderUpdateModel(int statusId, DateTime updated)
        {
            //Id = id;
            StatusId = statusId;
            Updated = updated;
        }

        //public int Id { get; set; }
        public int StatusId { get; set; }     
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
