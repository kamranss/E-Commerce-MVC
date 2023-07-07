using System.ComponentModel.DataAnnotations;

namespace AllUp2.ViewModels.AdminVM.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile[] Photo { get; set; }  // it means the files will be taken as assive and we can create multiple files at the same time
        public int Id { get; set; }

        public string? Title { get; set; }
        public string? Name { get; set; }
        public string? Suggestion { get; set; }
        public bool? IsMainPage { get; set; }
        public string? Description { get; set; }
    }
}
