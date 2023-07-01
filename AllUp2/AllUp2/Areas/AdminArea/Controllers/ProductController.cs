using AllUp2.Areas.AdminArea.Services.ProductS;
using Microsoft.AspNetCore.Mvc;

namespace AllUp2.Areas.AdminArea.Controllers
{
    
    [Area("AdminArea")]
    public class ProductController : Controller
    {
        private readonly IProService _proService;

        public ProductController(IProService proService)
        {
            _proService = proService;
        }

        public IActionResult Index(int page, int take = 3)
        {
            var products = _proService.GetProducts(page, take);
            return View(products);
        }
    }
}
