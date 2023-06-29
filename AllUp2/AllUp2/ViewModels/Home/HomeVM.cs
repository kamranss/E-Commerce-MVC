using AllUp2.Models;

namespace AllUp2.ViewModels.Home
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Expert> Experts { get; set; }
        public List<Post> Posts { get; set; }
        public List<Testimonial> Testimonials { get; set; }
    }
}
