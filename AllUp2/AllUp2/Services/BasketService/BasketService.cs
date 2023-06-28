using AllUp2.ViewModels;
using AllUp2.ViewModels.Home;
using Newtonsoft.Json;

namespace AllUp2.Services.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public BasketService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public int BasketCount()
        {
            // The request to the cookies method comes from IHttpContextAccessor
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];

            if (basket != null)
            {
                var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                //ViewBag.basketCount = products.Count;
                return products.Count();
            }

            return 0;
        }
    }
}
