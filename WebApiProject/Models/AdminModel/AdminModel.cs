namespace WebApiProject.Models.AdminModel
{
    public class AdminModel
    {
        public AdminModel(string name, string lastname, string email)
        {
            Name = name;
            Lastname = lastname;
            Email = email;
        }

        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

    }
}
