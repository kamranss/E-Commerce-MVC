using AllUp2.DAL;
using Microsoft.AspNetCore.Mvc;


namespace AllUp2.ViewComponents
{
    public class BBannerViewComponent:ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public BBannerViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var bbanners = _appDbContext.Banners.ToList();

            return View(await Task.FromResult(bbanners));
        }
    }
}
