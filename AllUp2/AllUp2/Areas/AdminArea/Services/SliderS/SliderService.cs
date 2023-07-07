using AllUp2.DAL;
using AllUp2.Helper;
using AllUp2.Models;
using AllUp2.ViewModels.Pagination;
using Microsoft.Extensions.Caching.Memory;

namespace AllUp2.Areas.AdminArea.Services.SliderS
{
    public class SliderService : ISliderS
    {
        private AppDbContext _appDbContext;
        private IMemoryCache _memoryCach;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SliderService(AppDbContext appDbContext, IMemoryCache memoryCach, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _memoryCach = memoryCach;
            _webHostEnvironment = webHostEnvironment;
        }

        public PaginationVM<Slider> GetSliders(int page, int take)
        {
            List<Slider> cachedSliders;
            bool SliderAlreadyExist = _memoryCach.TryGetValue("CachedSliders", out cachedSliders);


            if (!SliderAlreadyExist)
            {
                var query = _appDbContext.Sliders.AsQueryable();
                var slider = query.ToList();
                var cachEntryOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(10));
                _memoryCach.Set("CachedSliders", slider, cachEntryOption);

                List<Slider> newlyCachedSliders;
                _memoryCach.TryGetValue("CachedSliders", out newlyCachedSliders);

                int newlycachedpageCount = PageCount.PageCountt(newlyCachedSliders.Count(), take);
                cachedSliders = newlyCachedSliders
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();
                PaginationVM<Slider> cachedPaginationVM = new PaginationVM<Slider>(cachedSliders, page, newlycachedpageCount);

                return cachedPaginationVM;
            }

            int pageCount = PageCount.PageCountt(cachedSliders.Count(), take);
            var existSliders = cachedSliders
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();


            PaginationVM<Slider> paginationVM = new PaginationVM<Slider>(existSliders, page, pageCount);

            return paginationVM;
        }

    }
}
