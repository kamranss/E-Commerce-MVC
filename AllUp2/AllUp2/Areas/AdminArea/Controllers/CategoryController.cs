using AllUp2.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AllUp2.Areas.AdminArea.Controllers
{
    public class CategoryController : Controller
    {
        private AppDbContext _appDbContext;
        private IMemoryCache _memoryCach;

        public CategoryController(AppDbContext appDbContext, IMemoryCache memoryCach)
        {
            _appDbContext = appDbContext;
            _memoryCach = memoryCach;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
