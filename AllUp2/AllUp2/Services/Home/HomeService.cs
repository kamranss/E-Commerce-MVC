using AllUp2.DAL;
using AllUp2.ViewModels.Home;
using Microsoft.EntityFrameworkCore;

namespace AllUp2.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly AppDbContext _appDbContext;

        public HomeService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public HomeVM GetHomeContent()
        {
            HomeVM homeVM = new();
            homeVM.Sliders = _appDbContext.Sliders.Where(c => c.IsMainPage==true).ToList();  //  it measn we are talling to entity FrameWork that do not chase Sliders data --- When you will
            homeVM.Products = _appDbContext.Products.Include(p => p.Images).ToList();  // the include methods allows to add other ralted tables data it works like as join query 
            homeVM.Products = _appDbContext.Products.Include(p => p.Images).Take(8).AsNoTracking().ToList();
            homeVM.Categories = _appDbContext.Categories.Where(c => c.IsMain == true).ToList();
            homeVM.Blogs = _appDbContext.Blogs.OrderBy(b => b.Id).ToList();
            homeVM.Experts = _appDbContext.Experts.ToList();
            homeVM.Banners = _appDbContext.Banners.Where(b => b.IsFirst == true).ToList();
            homeVM.Posts = _appDbContext.Posts.ToList();
            homeVM.Testimonials = _appDbContext.Testimonials.ToList();

            return homeVM;
        }
    }
}
