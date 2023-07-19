using AllUp2.Areas.AdminArea.Services.CategoryS;
using AllUp2.DAL;
using AllUp2.Helper.FileExten;
using AllUp2.Models;
using AllUp2.ViewModels.AdminVM.Category;
using AllUp2.ViewModels.AdminVM.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;

namespace AllUp2.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    //[ValidateAntiForgeryToken]
    public class CategoryController : Controller
    {
        private AppDbContext _appDbContext;
        private ICategoryService _categoryService;
        private IMemoryCache _memoryCach;

        public CategoryController(AppDbContext appDbContext, IMemoryCache memoryCach, ICategoryService categoryService)
        {
            _appDbContext = appDbContext;
            _memoryCach = memoryCach;
            _categoryService = categoryService;
        }

        /*[AllowAnonymous] */// this meeans that anyone can access this end point
        public IActionResult Index()
        {
            //List<Category> cachedcategory;
            //bool categoryAlreadyExist = _memoryCach.TryGetValue("CachedCategory", out cachedcategory);
            var cachedcategory = _categoryService.GetCategoriesFromCache();
            bool categoryAlreadyExist = _categoryService.IsCacheCategoriesExist();

            if (!categoryAlreadyExist)
            {
                //var category = _appDbContext.Categories.ToList();
                var categories = _categoryService.GetAllCategories();

                //var cachEnteredOption = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(10));
                //_memoryCach.Set("CachedCategory", categories, cachEnteredOption);

                _categoryService.SetCategoriesToCache(categories);

                //_appDbContext.ChangeTracker.TrackGraph(category, e => e.Entry.State = EntityState.Detached);
                return View(categories);
            }
            else
            {
                return View(cachedcategory);
            }
           
        } // done
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            //var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            var category = _categoryService.FindCategory(id);
            if (category == null) return NotFound();
            return View(category);
        }// done
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_appDbContext.Categories.ToList(), "Id", "Name");
            return View();
        } // done
        [HttpPost]
        public IActionResult Create(CategoryCreateVM category) //string name, string description
        {
            if (!ModelState.IsValid) return View();
            //var exist = _appDbContext.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            var existWithSameName = _categoryService.IsCategoryWithThisNameExist(category.Name);
            if (existWithSameName)
            {
                ModelState.AddModelError("Name", "In the database the category with the same Name already exist!");
                TempData["error"] = "Not Succefull";
                return View();
              
            }
            //Category newcategory = new Category()
            //{
            //    Name = category.Name,
            //    Description = category.Description,
            //};

            var newCategory = _categoryService.MapCategory(category);

            //_appDbContext.Categories.Add(newCategory);
            _categoryService.AddCategory(newCategory);
            //_appDbContext.SaveChanges();
            _categoryService.SaveChanges();
            bool exist = _categoryService.IsCacheCategoriesExist();
            if (!exist)
            {
                var categories = _categoryService.GetAllCategories();
                _categoryService.SetCategoriesToCache(categories);
                TempData["success"] = "Created Succesfully";
                return RedirectToAction(nameof(Index));
            }
            var cachedcategory = _categoryService.GetCategoriesFromCache();
            _categoryService.AddCategoryToList(cachedcategory, newCategory);
            _categoryService.SetCategoriesToCache(cachedcategory);

            TempData["success"] = "Created Succesfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {

            if (id == null) return NotFound();
            var category = _categoryService.FindCategory(id);
            if (category == null) return NotFound();
            
            return View(new CategoryUpdateVM { Name = category.Name, Description = category.Description }); // not finished yet
        } // done
        [HttpPost]
        public IActionResult Update(int? id, CategoryUpdateVM categoryUpdateVM)
        {
            if (id == null) return NotFound();
            if (!ModelState.IsValid)
            {
                return View();
            }

            var category = _categoryService.FindCategory(id);
            if (category == null) return NotFound();
            var exist = _categoryService.IsCategoryWithThisNameExist(categoryUpdateVM.Name);

            if (exist)
            {
                ModelState.AddModelError("Name", "Eyni adli category movcuddur");
                return View();
            }
            if (categoryUpdateVM.NewImages != null)
            {
                foreach (var newImage in categoryUpdateVM.NewImages)
                {
                    if (!newImage.CheckFileType())
                    {
                        ModelState.AddModelError("NewImages", "Choose a picture");
                        return View(categoryUpdateVM);
                    }
                    if (newImage.CheckFileLenght(1000))
                    {
                        ModelState.AddModelError("NewImages", "Big size");
                        return View(categoryUpdateVM);
                    }

                    //Image image = new();
                    //image.ImageUrl = newImage.SaveImage(_webHostEnvironment, "img");
                    //product.Images.Add(image);

                    _categoryService.SaveCategoryImage(newImage, category);
                }
            }
            //category.Name = categoryUpdateVM.Name;
            //category.Description = categoryUpdateVM.Description;
            _categoryService.MapUpdateCategory(category, categoryUpdateVM);
            _categoryService.SaveChanges();
            //var cachedCategory = _memoryCach.Get<List<Category>>("CachedCategory");
            var cachedCategory = _categoryService.GetCategoriesFromCache();
            if (cachedCategory != null)
            {
                //var existCategory = cachedCategory.FirstOrDefault(category => category.Id == id);
                var existCategory = _categoryService.FindCategoryFromList(category.Id, cachedCategory);
                if (existCategory != null)
                {
                    //existCategory.Name = category.Name;
                    //existCategory.Description = category.Description;
                    _categoryService.MapUpdateCacheCategory(existCategory, category);
                }
                //cachedCategory.Add(category);
                //_memoryCach.Set("CachedCategory", cachedCategory);
                _categoryService.SetCategoriesToCache(cachedCategory);
            }

            return RedirectToAction(nameof(Update), "category");

        } // done

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var category = _categoryService.FindCategory(id);
            if (category == null) return NotFound();
            //_appDbContext.Categories.Remove(category);
            _categoryService.RemoveCategory(category);
            //int ruselt = _appDbContext.SaveChanges(true);
            var result = _categoryService.SaveChangesResult();
            var isCacheExist = _categoryService.IsCacheCategoriesExist();
            if (isCacheExist)
            {
                var existCategories = _categoryService.GetCategoriesFromCache();
                var cacheCategory = _categoryService.FindCategoryFromList(id, existCategories);
                if (cacheCategory != null)
                {
                    existCategories.Remove(cacheCategory);
                    _categoryService.SetCategoriesToCache(existCategories);
                }
            }
            if (result > 0) { return RedirectToAction(nameof(Index)); }
            return NotFound();

        } // done
    }
}
