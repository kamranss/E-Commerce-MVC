using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AllUp2.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class DashboardController : Controller
    {

        //[Authorize(Roles = "Admin")]
    
        public IActionResult Index()
        {
            return View();
        }
    }
}
