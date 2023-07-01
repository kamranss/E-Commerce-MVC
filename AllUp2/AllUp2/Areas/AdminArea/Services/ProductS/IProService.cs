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
        void Create(ProductCreateVM productCreateVM);
        void Delete(int? id);
        ProductUpdateVM UpdateProPage(int? id);
        void UpdateProDB(ProductUpdateVM productUpdateVM);
        void RemoveImage(int? id);

        Product Deatil(int? id);



    }
}
