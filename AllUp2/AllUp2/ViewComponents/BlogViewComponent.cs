using AllUp2.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AllUp2.ViewComponents
{
    public class BlogViewComponent:ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public BlogViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(string viewName, int? take = null, int skip = 0)
        {
            int defaultTake = _appDbContext.Blogs.Count();
            int takeValue = take ?? defaultTake;

            var blogs = _appDbContext.Blogs.Include(b => b.BlogDetail).Skip(skip).Take(takeValue).ToList();
            return View(viewName, await Task.FromResult(blogs));
        }
    }
}
