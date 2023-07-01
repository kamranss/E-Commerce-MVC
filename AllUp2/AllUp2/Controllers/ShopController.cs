using Microsoft.AspNetCore.Mvc;

namespace AllUp2.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
