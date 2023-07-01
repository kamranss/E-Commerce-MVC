using System.ComponentModel.DataAnnotations;

namespace AllUp2.ViewModels.AdminVM.Product
{
    public class ProductCreateVM
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public IFormFile[] Images { get; set; }
    }
}
