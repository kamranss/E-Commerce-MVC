using AllUp2.DAL;
using AllUp2.Models;
using AllUp2.ViewModels.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllUp2.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;

        public HeaderViewComponent(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.UserFullName = "";
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.UserFullName = user.FullName;
            }

          BioVM bioVM = new BioVM();
            var Bio= _appDbContext.Bios.ToList();
            var brands = _appDbContext.Brands.ToList();
            var services = _appDbContext.Services.ToList();
            var icons = _appDbContext.Icons.ToList();
            bioVM.Icons = icons;
            bioVM.Brands = brands;
            bioVM.Services = services;
            bioVM.Bios = Bio;

            return View(await Task.FromResult(bioVM));
        }
    }
}
