namespace WebApiProject.Models.CategoryModels
{
    public class CategoryCreateModel
    {
        private string categoryName;
        public CategoryCreateModel(string categoryName)
        {
            CategoryName = categoryName;
        }

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value.Trim(); }
        }

    }
}
