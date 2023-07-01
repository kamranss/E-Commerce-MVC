using Microsoft.AspNetCore.Mvc;

namespace AllUp2.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
