namespace WebApiProject.Models.CategoryModels
{
    public class CategoryModel
    {
        public CategoryModel(string categoryName)
        {
            CategoryName = categoryName;
        }

        public CategoryModel(int id, string categoryName)
        {
            Id = id;
            CategoryName = categoryName;
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }

    }
}
