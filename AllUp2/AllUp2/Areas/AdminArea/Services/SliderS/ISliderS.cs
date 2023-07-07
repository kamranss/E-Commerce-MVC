using AllUp2.DAL;
using AllUp2.Helper;
using AllUp2.Models;
using AllUp2.ViewModels.Pagination;
using Microsoft.Extensions.Caching.Memory;

namespace AllUp2.Areas.AdminArea.Services.SliderS
{
    public interface ISliderS
    {
        public PaginationVM<Slider> GetSliders(int page, int take);
    }
}
