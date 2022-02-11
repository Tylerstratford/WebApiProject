namespace WebApiProject.Models.StatusModels
{
    public class StatusModel
    {
        public StatusModel(int id, string status)
        {
            Id = id;
            Status = status;
        }

        public int Id { get; set; }
        public string Status { get; set; }

    }
}
