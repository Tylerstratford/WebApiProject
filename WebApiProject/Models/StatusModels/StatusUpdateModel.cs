namespace WebApiProject.Models.StatusModels
{
    public class StatusUpdateModel
    {
        private string status;
        public StatusUpdateModel(int id, string status)
        {
            Id = id;
            Status = status;
        }

        public int Id { get; set; }
        public string Status
        {
            get { return status; }
            set { status = value.Trim(); }
        }
    }
}
