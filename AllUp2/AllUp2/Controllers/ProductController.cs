using AllUp2.DAL;
using AllUp2.Services.ProductS;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AllUp2.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
          var products = _productService.MainProducts();
          ViewBag.ProductsCount =products.Count();

            return View(products);
        }
        public IActionResult LoadMore(int skip)
        {
           var products = _productService.LoadMore(skip);

            return PartialView("_LoadMorePartial", products);
        }
        public IActionResult Search(string search)
        {
          var products = _productService.Search(search);

            return PartialView("_SearchPartial", products);
        }
    }
}
