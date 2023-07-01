using AllUp2.DAL;
using AllUp2.Helper;
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
        public void Create(ProductCreateVM productCreateVM)
        {
            throw new NotImplementedException();
        }

        public Product Deatil(int? id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetCategories()
        {
            throw new NotImplementedException();
        }

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
        } // done

        public void RemoveImage(int? id)
        {
            throw new NotImplementedException();
        }

        public void UpdateProDB(ProductUpdateVM productUpdateVM)
        {
            throw new NotImplementedException();
        }

        public ProductUpdateVM UpdateProPage(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
