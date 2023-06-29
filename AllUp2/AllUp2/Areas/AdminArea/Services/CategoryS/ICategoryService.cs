using AllUp2.Models;

namespace AllUp2.Areas.AdminArea.Services.CategoryS
{
    public interface ICategoryService
    {
        public Category GetCategory(int id);
        public Category GetCategoryByName(string categoryName);
        public List<Category> GetAllCategories();
        public Category DeleteCAtegory(int id);
        public Category UpdateCategory(int id);

    }
}
