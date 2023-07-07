using AllUp2.Services.AboutS;
using AllUp2.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace AllUp2.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public IActionResult Index()
        {
           var aboutInfo =  _aboutService.GeAboutInfo();
            AboutVM newAboutVM = new AboutVM();
           newAboutVM = _aboutService.MapAboutInfoToVM(aboutInfo);
            return View(newAboutVM);
        }
    }
}
