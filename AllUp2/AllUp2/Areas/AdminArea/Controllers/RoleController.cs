using Microsoft.AspNetCore.Mvc;

namespace AllUp2.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
