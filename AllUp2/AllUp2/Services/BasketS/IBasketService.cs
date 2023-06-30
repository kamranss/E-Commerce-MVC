using AllUp2.Models;
using AllUp2.ViewModels.Home;

namespace AllUp2.Services.BasketS
{
    public interface IBasketService
    {
        int BasketCount();
        List<BasketVM> ShowBAsket(string basket);
        Product GetProduct(int? id);
        List<BasketVM> CheckBasketProduct(string basket, Product product);
        string GetBasket();
        void RemoveProductFromBasket(int? id, string basket);
        void AddProductToBasket(Product products);
        void IncDecProductFromBasket(int? id, int count);
    }
}
