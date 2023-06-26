using Microsoft.AspNetCore.Mvc;

namespace Allup.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View(await Task.FromResult("salam"));
        }
    }
}
