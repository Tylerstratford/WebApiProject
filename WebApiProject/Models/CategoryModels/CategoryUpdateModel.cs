namespace WebApiProject.Models.CategoryModels
{
    public class CategoryUpdateModel
    {
        public CategoryUpdateModel(int id, string categoryName)
        {
            Id = id;
            CategoryName = categoryName;
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
