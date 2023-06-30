using AllUp2.Models;

namespace AllUp2.Services.ProductS
{
    public interface IProductService
    {
        int Sum(int x, int y);

        List<Product> MainProducts();
        List<Product> Search(string search);
        List<Product> LoadMore(int skip);
    }
}
