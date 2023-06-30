using Microsoft.AspNetCore.Mvc;

namespace AllUp2.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
