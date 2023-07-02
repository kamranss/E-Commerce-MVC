using AllUp2.Models;
using AllUp2.ViewModels.AdminVM.Product;
using AllUp2.ViewModels.Home;
using AllUp2.ViewModels.Pagination;

namespace AllUp2.Areas.AdminArea.Services.ProductS
{
    public interface IProService
    {
        PaginationVM<Product> GetProducts(int page, int take);
        List<Category> GetCategories();
        void Create(Product product);
        void Delete(int? id);
        Product FindProduct(int? id);
        ProductUpdateVM MapProducTotVM(Product product);

        //void RemoveImage(int? id);

        Product Deatil(int? id);

        void SaveProImage(IFormFile newImage, Product product);
        void SaveChanges();
        Product IsProductExist(int? id);
        Image FindImage(int? id);
        void RemoveProImage(Image image);



    }
}
