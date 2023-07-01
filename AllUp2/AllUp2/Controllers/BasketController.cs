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
            var products = _basketService.AddProductToBasketList(basket, product);
            _basketService.AppendListToBasket(products);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ShowBasket()
        {
            string basket = _basketService.GetBasket();
            var products = _basketService.ShowBAsket(basket);
            return Json(products);
        }

        public IActionResult RemoveProduct(int? id)
        {
            if (id == null) return NotFound();

            string basket = _basketService.GetBasket();

            _basketService.RemoveProductFromBasket(id, basket);

            return RedirectToAction("ShowBasket");
        }

        public IActionResult MinusPlusProductCount(int? id, int count)
        {
            if (id == null) return NotFound();

            string basket = _basketService.GetBasket();

            _basketService.IncDecProductFromBasket(id, count, basket);

            return RedirectToAction("ShowBasket");
        }
    }
}
