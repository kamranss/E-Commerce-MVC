using Microsoft.AspNetCore.Mvc;

namespace AllUp2.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
