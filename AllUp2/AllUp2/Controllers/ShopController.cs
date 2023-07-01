using AllUp2.Services.ProductS;
using Microsoft.AspNetCore.Mvc;

namespace AllUp2.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.MainProducts();
            ViewBag.ProductsCount = products.Count();

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
