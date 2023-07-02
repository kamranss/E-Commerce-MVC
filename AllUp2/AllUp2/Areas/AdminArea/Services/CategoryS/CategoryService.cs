using AllUp2.DAL;
using AllUp2.Models;
using AllUp2.ViewModels.AdminVM.Category;
using Microsoft.Extensions.Caching.Memory;

namespace AllUp2.Areas.AdminArea.Services.CategoryS
{
    public class CategoryService : ICategoryService
    {
        private IMemoryCache _memoryCach;
        private readonly AppDbContext _appDbContext;

        public CategoryService(IMemoryCache memoryCach, AppDbContext appDbContext)
        {
            _memoryCach = memoryCach;
            _appDbContext = appDbContext;
        }

        public void AddCategoriesToCache(List<Category> category)
        {
            throw new NotImplementedException();
        }

        public void AddCategory(Category newCategory)
        {
            _appDbContext.Categories.Add(newCategory);
        } // adding category to Db

        public void AddCategoryToCache(Category category)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategoryFromCache(Category category)
        {
            throw new NotImplementedException();
        }

        public Category FindCategory(int? id)
        {
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            return category;
        } // done getting specific category from Db

        public List<Category> GetAllCategories()
        {
            var categories = _appDbContext.Categories.ToList();
            return categories;
        } // done getting all categories from Db

        public List<Category> GetCategoriesFromCache()
        {
            List<Category> cachedcategory;
            _memoryCach.TryGetValue("CachedCategory", out cachedcategory);
            return cachedcategory;
        }  // done getting all categories from cache

        public bool IsCategoryWithThisNameExist(string categoryName) // done -- checking whether the category with the same name exist
        {
            var exist = _appDbContext.Categories.Any(c => c.Name.ToLower() == categoryName.ToLower());
            return exist;
        }

        public void GetCategoryFromCache(Category category)
        {
            Category cachedcategory;
             _memoryCach.TryGetValue("CachedCategory", out cachedcategory);
            
        } // useless

        public bool IsCacheCategoriesExist() // dine checking whether category cache exist or not
        {
            List<Category> cachedcategory;
            bool categoryAlreadyExist = _memoryCach.TryGetValue("CachedCategory", out cachedcategory);
            return categoryAlreadyExist;
        }

        public Category IsSameCategoryExist(Category category)
        {
            throw new NotImplementedException();
        } // dublicate should be removed

        public void MapUpdatedCategory(Category category, CategoryUpdateVM categoryUpdateVM)
        {
            throw new NotImplementedException();
        } // Not used

        public void RemoveCategory(Category category)
        {
            _appDbContext.Categories.Remove(category);
        } // done removing category from Db

        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        } // done saving implented changes into Db

        public Category UpdateCategory(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatedCategoryInCache(Category category)
        {
            throw new NotImplementedException();
        }

        public void SetCategoriesToCache(List<Category> categories)
        {
            var cachEnteredOption = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(10));
            _memoryCach.Set("CachedCategory", categories, cachEnteredOption);
        } // done setting categories to Cache

        public void MapUpdateCategory(Category category, CategoryUpdateVM categoryUpdateVM) // done partially but category have more fields
        {
            category.Name = categoryUpdateVM.Name;
            category.Description = categoryUpdateVM.Description;
        }

        public Category MapCategory(CategoryCreateVM categoryCreateVM)  // done mapping category from VM to Model
        {
            Category newcategory = new Category()
            {
                Name = categoryCreateVM.Name,
                Description = categoryCreateVM.Description,
            };
            return newcategory;
        }

        public void AddCategoryToList(List<Category> categoriesList, Category category) // done adding one category into already exist List
        {
            categoriesList.Add(category);
        }

        public Category FindCategoryFromList(int? id, List<Category> categories)
        {
            var existCategory = categories.FirstOrDefault(category => category.Id == id);
            return existCategory;
        } // done finding category from the list -- this is designed for cached categories

        public void MapUpdateCacheCategory(Category existCategory, Category category)
        {
            existCategory.Name = category.Name;
            existCategory.Description = category.Description;
        } // used in one place not important

        public int SaveChangesResult()
        {
            int rusult = _appDbContext.SaveChanges(true);
            return rusult;
        } // done saving changes and getting result for validation
    }
}
