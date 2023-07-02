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

        public void AddCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void AddCategoryToCache(Category category)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategoryFromCache(Category category)
        {
            throw new NotImplementedException();
        }

        public Category FindCategory(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAllCategories()
        {
            var categories = _appDbContext.Categories.ToList();
            return categories;
        }

        public List<Category> GetCategoriesFromCache()
        {
            List<Category> cachedcategory;
            _memoryCach.TryGetValue("CachedCategory", out cachedcategory);
            return cachedcategory;
        }  // done getting all categories from cache

        public Category GetCategoryByName(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void GetCategoryFromCache(Category category)
        {
            throw new NotImplementedException();
        }

        public bool IsCacheCategoriesExist() // dine checking whether category cache exist or not
        {
            List<Category> cachedcategory;
            bool categoryAlreadyExist = _memoryCach.TryGetValue("CachedCategory", out cachedcategory);
            return categoryAlreadyExist;
        }

        public Category IsSameCategoryExist(Category category)
        {
            throw new NotImplementedException();
        }

        public void MapUpdatedCategory(Category category, CategoryUpdateVM categoryUpdateVM)
        {
            throw new NotImplementedException();
        }

        public int RemoveCAtegory(Category category)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

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
    }
}
