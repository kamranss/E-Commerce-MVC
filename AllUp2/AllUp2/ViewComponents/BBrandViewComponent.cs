using AllUp2.DAL;
using Microsoft.AspNetCore.Mvc;

namespace AllUp2.ViewComponents
{
    public class BBrandViewComponent: ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public BBrandViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = _appDbContext.Brands.ToList();
            return View(await Task.FromResult(brands));
        }
    }
}
