using AllUp2.Models;

namespace AllUp2.ViewModels.AdminVM.Product
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int? Count { get; set; }
        public int? CategoryId { get; set; }
        public List<Image>? ExistingImages { get; set; }
        public List<IFormFile>? NewImages { get; set; }

        public ProductUpdateVM()
        {
            ExistingImages = new List<Image>();
        }
    }
}
