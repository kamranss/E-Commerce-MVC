using AllUp2.Helper.FileExten;
using AllUp2.Models;
using AllUp2.ViewModels.AdminVM.Category;
using Microsoft.AspNetCore.Hosting;

namespace AllUp2.Areas.AdminArea.Services.CategoryS
{
    public interface ICategoryService
    {
        public Category FindCategory(int? id);
        public Category FindCategoryFromList(int? id, List<Category> categories );
        public bool IsCategoryWithThisNameExist(string categoryName);
        public Category IsSameCategoryExist(Category category);
        public List<Category> GetAllCategories();
        public void SetCategoriesToCache(List<Category> categories);
        public void RemoveCategory(Category category);
        public void AddCategory(Category newCategory);
        public void UpdatedCategoryInCache(Category category);
        public void DeleteCategoryFromCache(Category category);
        public void AddCategoryToCache(Category category);
        public void GetCategoryFromCache(Category category);
        public List<Category> GetCategoriesFromCache();
        public bool IsCacheCategoriesExist();
        public void AddCategoriesToCache(List<Category> category);
        public void SaveChanges();
        public int SaveChangesResult();
        public void MapUpdateCategory(Category category, CategoryUpdateVM categoryUpdateVM);
        public void MapUpdateCacheCategory(Category existCategory, Category category);
        public Category MapCategory(CategoryCreateVM categoryCreateVM);

        public void AddCategoryToList(List<Category> categoriesList, Category category);
        public Category UpdateCategory(int id);

        public void SaveCategoryImage(IFormFile newImage, Category category);
      
    }
}
