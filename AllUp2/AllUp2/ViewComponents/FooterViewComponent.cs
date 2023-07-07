using AllUp2.DAL;
using AllUp2.Models;
using AllUp2.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace AllUp2.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly AppDbContext _appDbContext;


        public FooterViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            BioVM bioVM = new BioVM();
            var Bio = _appDbContext.Bios.ToList();
            var brands = _appDbContext.Brands.ToList();
            var services = _appDbContext.Services.ToList();
            var icons =_appDbContext.Icons.ToList();
            bioVM.Brands = brands;
            bioVM.Services = services;
            bioVM.Bios = Bio;
            bioVM.Icons = icons;
            return View(await Task.FromResult(bioVM));
        }
    }
}
