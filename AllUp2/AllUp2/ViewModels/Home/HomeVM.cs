using AllUp2.Models;

namespace AllUp2.ViewModels.Home
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Banner> Banners { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Expert> Experts { get; set; }
        public List<Post> Posts { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Service> Services { get; set; }

        public HomeVM()
        {

            Sliders = new List<Slider>();
            Products = new List<Product>();
            Categories = new List<Category>();
            Banners = new List<Banner>();
            Experts = new List<Expert>();
            Posts = new List<Post>();
            Testimonials = new List<Testimonial>();
            Services = new List<Service>();

        }
    }
}
