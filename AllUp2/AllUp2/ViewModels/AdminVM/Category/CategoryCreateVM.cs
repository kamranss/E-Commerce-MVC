using System.ComponentModel.DataAnnotations;

namespace AllUp2.ViewModels.AdminVM.Category
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "Bosh qoyma")]
        [MaxLength(10)]
        public string Name { get; set; }
        [Required(ErrorMessage = "bosh qoyma")]
        [MaxLength(50, ErrorMessage = "50 den kicik ola bilmez")]
        public string Description { get; set; }
        //public int ParentCategoryId { get; set; }
        //public IFormFile Image { get; set; }
        //public string ImageURL { get; set; }
    }
}
