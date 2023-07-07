using System.ComponentModel.DataAnnotations;

namespace AllUp2.ViewModels.AdminVM.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile[] Photo { get; set; }  // it means the files will be taken as assive and we can create multiple files at the same time
    }
}
