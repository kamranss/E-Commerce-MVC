using System.ComponentModel.DataAnnotations;

namespace AllUp2.ViewModels.AdminVM.Category
{
    public class CategoryUpdateVM
    {
        [Required(ErrorMessage = "Bosh qoyma")]
        [MaxLength(10)]
        public string Name { get; set; }
        [Required(ErrorMessage = "bosh qoyma")]
        [MaxLength(150, ErrorMessage = "50 den kicik ola bilmez")]
        public string Description { get; set; }
    }
}
