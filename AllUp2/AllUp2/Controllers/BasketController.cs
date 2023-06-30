using AllUp2.DAL;
using AllUp2.Models;
using AllUp2.Services.BasketS;
using AllUp2.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AllUp2.Controllers
{
    public class BasketController : Controller
    {
        private IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBasket(int? id)
        {
            if (id == null) return NotFound();
            var product = _basketService.GetProduct(id);
            if (product == null) return NotFound();
            string basket = _basketService.GetBasket();
            var products = _basketService.CheckBasketProduct(basket, product);
            _basketService.AddProductToBasket(product);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ShowBasket()
        {
            string basket = _basketService.GetBasket();
            var products = _basketService.ShowBAsket(basket);
            return Json(products);
        }
    }
}
