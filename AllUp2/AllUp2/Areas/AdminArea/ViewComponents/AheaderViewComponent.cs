using Microsoft.AspNetCore.Mvc;

namespace AllUp2.Areas.AdminArea.ViewComponents
{
    public class AheaderViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View(await Task.FromResult(""));
        }
    }
}
