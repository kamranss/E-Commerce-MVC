using AllUp2.Models;
using AllUp2.ViewModels.Home;

namespace AllUp2.Services.BasketS
{
    public interface IBasketService
    {
        int BasketCount();
        List<BasketVM> ShowBAsket(string basket);
        Product GetProduct(int? id);
        List<BasketVM> AddProductToBasketList(string basket, Product product);
        string GetBasket();
        void RemoveProductFromBasket(int? id, string basket);
        void AppendListToBasket(List<BasketVM> products);
        void IncDecProductFromBasket(int? id, int count, string basket);
    }
}
