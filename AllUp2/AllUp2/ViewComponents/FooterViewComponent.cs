using Microsoft.AspNetCore.Mvc;

namespace AllUp2.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View(await Task.FromResult(""));
        }
    }
}
