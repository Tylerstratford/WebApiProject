namespace WebApiProject.Models.StatusModels
{
    public class StatusCreateModel
    {

        private string status;

        public StatusCreateModel(string status)
        {
            Status = status;
        }

        public string Status
        {
            get { return status; }
            set { status = value.Trim(); }
        }
    }
}
