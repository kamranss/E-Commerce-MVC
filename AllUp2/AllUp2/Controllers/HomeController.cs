
using AllUp2.DAL;
using AllUp2.Services.AccountS;
using AllUp2.Services.Home;
using AllUp2.Services.ProductS;
using AllUp2.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AllUp2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IProductService _iporduct;
        private readonly AccountService _account;
        private readonly IHomeService _homeService;

        public HomeController(AppDbContext appDbContext, IProductService iporduct, AccountService account, IHomeService homeService)
        {
            _appDbContext = appDbContext;
            _iporduct = iporduct;
            _account = account;
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            

            var homeContent = _homeService.GetHomeContent();

            var datas = _appDbContext.ChangeTracker.Entries().ToList(); // it gives you all traked data

            _appDbContext.ChangeTracker.DetectChanges(); // detects last changes -- it is  by default initiated by entityFramework itself
            /*_appDbContext.ChangeTracker.AutoDetectChangesEnabled.; */  // this is blocking entity framework by default calling the DetectChanges
            _appDbContext.SaveChanges(); // There is all ASync way for saving changes it can be implemented by calling SaveChangesAsync() method
                                         //_appDbContext.SaveChangesAsync();


            ViewBag.ProductsCount = _appDbContext.Products.Count();
            //homeVM.Blogs = _appDbContext.Blogs.Skip(1).Take(2).ToList();
           
            var result = _iporduct.Sum(2, 4);
            _account.Login("...", "..."); // for test purposes

            return View(homeContent);
        }

    }
}