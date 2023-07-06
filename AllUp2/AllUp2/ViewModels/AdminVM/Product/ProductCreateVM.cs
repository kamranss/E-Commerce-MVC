using System.ComponentModel.DataAnnotations;

namespace AllUp2.ViewModels.AdminVM.Product
{
    public class ProductCreateVM
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int Count { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public IFormFile[] Images { get; set; }
    }
}
