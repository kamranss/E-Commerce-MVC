using AllUp2.DAL;
using AllUp2.Models;
using Microsoft.EntityFrameworkCore;

namespace AllUp2.Services.ProductS
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _appDbContext;

        public ProductService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<Product> LoadMore(int skip)
        {
            var products = _appDbContext.Products
               .Include(p => p.Category)
               .Include(p => p.Images)
               .Skip(skip)
               .Take(2)
               .ToList();
            return products;
        }

        public List<Product> MainProducts()
        {
            var query = _appDbContext.Products.AsQueryable();
            var products = query.Include(p => p.Category)
                .Include(p => p.Images)
                .Take(2)
                .ToList();
            return products;
        }

        public List<Product> Search(string search)
        {
            var products = _appDbContext.Products
                 .Where(p => p.Name.ToLower()
                 .Contains(search.ToLower()))
                 .Take(2)
                 .OrderByDescending(p => p.Id)
                 .ToList();
            return products;
        }

        public int Sum(int x, int y)
        {
            return x + y;
        }
    }
}
