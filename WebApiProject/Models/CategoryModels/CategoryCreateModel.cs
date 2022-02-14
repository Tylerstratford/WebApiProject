namespace WebApiProject.Models.CategoryModels
{
    public class CategoryCreateModel
    {
        public CategoryCreateModel(string categoryName)
        {
            CategoryName = categoryName;
        }

        public string CategoryName { get; set; }

    }
}
