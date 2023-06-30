using AllUp2.DAL;
using AllUp2.Models;
using AllUp2.ViewModels.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;

namespace AllUp2.Services.BasketS
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppDbContext _appDbContext;

        public BasketService(IHttpContextAccessor contextAccessor, AppDbContext appDbContext)
        {
            _contextAccessor = contextAccessor;
            _appDbContext = appDbContext;
        }
        public List<BasketVM> AddProductToBasketList(string basket, Product product)
        {
            List<BasketVM> products;

            if (basket == null)
            {
                products = new List<BasketVM>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }
            var existProduct = products.Find(p => p.Id == product.Id);
            if (existProduct == null)
            {
                BasketVM basketVM = new()
                {
                    Id = product.Id,
                    BasketCount = 1
                };
                products.Add(basketVM);
            }
            else
            {
                existProduct.BasketCount++;
            }
            return products;
        }

        public void AppendListToBasket(List<BasketVM> products)
        {
            _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(10) });
        }

        public int BasketCount()
        {
            // The request to the cookies method comes from IHttpContextAccessor
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];

            if (basket != null)
            {
                var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                return products.Count();
            }

            return 0;
        }

        public string GetBasket()
        {
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            return basket;
        }

        public Product GetProduct(int? id)
        {
            var product = _appDbContext.Products.Include(product => product.Images).FirstOrDefault(p => p.Id == id);

            return product;
        }

        public void IncDecProductFromBasket(int? id, int count, string basket)
        {
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            var existProduct = products.Find(p => p.Id == id);

            if (existProduct != null)
            {
                existProduct.BasketCount = count;

                if (existProduct.BasketCount <= 0)
                {
                    products.Remove(existProduct);
                }
            }

            _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(10) });
        }

        public void RemoveProductFromBasket(int? id, string basket)
        {
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            var existProduct = products.Find(p => p.Id == id);
            products.Remove(existProduct);
            _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(10) });
        }

        public List<BasketVM> ShowBAsket(string basket)
        {
            List<BasketVM> products;
            if (basket == null)
            {
                products = new List<BasketVM>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

                foreach (var item in products)
                {
                    Product existProduct = _appDbContext.Products
                        .Include(p => p.Images)
                        .FirstOrDefault(p => p.Id == item.Id);

                    item.Name = existProduct.Name;
                    item.Price = existProduct.Price;
                    item.ImageUrl = existProduct.Images.FirstOrDefault().ImageUrl;
                }
            }
            return products;
        }
    }
}
