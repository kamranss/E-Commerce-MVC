using AllUp2.DAL;
using AllUp2.Helper;
using AllUp2.Helper.FileExten;
using AllUp2.Models;
using AllUp2.ViewModels.AdminVM.Product;
using AllUp2.ViewModels.Home;
using AllUp2.ViewModels.Pagination;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;


namespace AllUp2.Areas.AdminArea.Services.ProductS
{
    public class ProService : IProService
    {
        private AppDbContext _appDbContext;
        private IMemoryCache _memoryCach;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProService (AppDbContext appDbContext, IMemoryCache memoryCash, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _memoryCach = memoryCash;
            _webHostEnvironment = webHostEnvironment;
        }
        public void Create(Product product)
        {

            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();
           
            List<Product> cachedProduct;
            bool ProductAlreadyExist = _memoryCach.TryGetValue("CachedProducts", out cachedProduct);
            if (!ProductAlreadyExist)
            {
                var productt = _appDbContext.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .ToList();
                var cachEntryOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(10));
                _memoryCach.Set("CachedProducts", product, cachEntryOption);
            }
            else
            {
                // Cached products exist, retrieve them
                var products = cachedProduct;

                // Add the newly added product to the existing products list
                var newProduct = _appDbContext.Products
                    .Include(p => p.Images)
                    .Include(p => p.Category)
                    .SingleOrDefault(p => p.Id == product.Id); // Replace 'newProductId' with the actual ID of the newly added product

                if (newProduct != null)
                {
                    products.Add(newProduct);

                    // Update the cache with the updated products list
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromDays(10));
                    _memoryCach.Set("CachedProducts", products, cacheEntryOptions);
                }
            }
        }// done

        public Product Deatil(int? id)
        {
            var product = _appDbContext.Products
           .Include(p => p.Category)
           .Include(p => p.Images)
           .FirstOrDefault(p => p.Id == id);

            return product;
        } // done

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetCategories()
        {
            List<Category> categories= (_appDbContext.Categories.ToList());
            return categories;
        } // done referring to create action without Post method

        public PaginationVM<Product> GetProducts(int page, int take)
        {
            List<Product> cachedProduct;
            bool ProductAlreadyExist = _memoryCach.TryGetValue("CachedProducts", out cachedProduct);


            if (!ProductAlreadyExist)
            {
                var query = _appDbContext.Products.AsQueryable();
                var product = query
                .Include(p => p.Images)
                .Include(p => p.Category)
                .ToList();
                var cachEntryOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(10));
                _memoryCach.Set("CachedProducts", product, cachEntryOption);

                List<Product> newlycachedProduct;
                _memoryCach.TryGetValue("CachedProducts", out newlycachedProduct);

                int newlycachedpageCount = PageCount.PageCountt(newlycachedProduct.Count(), take);
                cachedProduct = newlycachedProduct
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();
                PaginationVM<Product> cachedPaginationVM = new PaginationVM<Product>(cachedProduct, page, newlycachedpageCount);


                return cachedPaginationVM;
            }

            int pageCount = PageCount.PageCountt(cachedProduct.Count(), take);
            var existProduct = cachedProduct
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();


            PaginationVM<Product> paginationVM = new PaginationVM<Product>(existProduct, page, pageCount);

            return paginationVM;
        } // done referring to Index page

        //public void RemoveImage(int? id)
        //{
           
        //}

        public ProductUpdateVM MapProducTotVM(Product product)
        {
            var productUpdateVM = new ProductUpdateVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Count = product.Count,
                CategoryId = product.CategoryId,
                ExistingImages = product.Images.ToList()
            };
            return productUpdateVM;
        } // done refer to the Update page not Post

        public Product FindProduct(int? id)
        {
            var product = _appDbContext.Products
               .Include(p => p.Images)
               .FirstOrDefault(c => c.Id == id);
            return product;

        } // done refer to Update page not Post

        public void SaveProImage(IFormFile newImage, Product product)
        {
            Image image = new();
            image.ImageUrl = newImage.SaveImage(_webHostEnvironment, "img");
            product.Images.Add(image);
        }// done save updated product images 

        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        } // done _AppDbContext save changes

        public Product IsProductExist(int? id)
        {
            var product = _appDbContext.Products.FirstOrDefault(c => c.Id == id);
            return product;
        } // done checkes whether product exist in DB

        public Image FindImage(int? id)
        {
            var image = _appDbContext.Images.FirstOrDefault(i => i.Id == id);
            return image;
        }

        public void RemoveProImage(Image image)
        {
            _appDbContext.Images.Remove(image);
            _appDbContext.SaveChanges();
        }
    }
}
