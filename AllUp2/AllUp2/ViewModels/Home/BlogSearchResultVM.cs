using AllUp2.Models;

namespace AllUp2.ViewModels.Home
{
    public class BlogSearchResultVM
    {
        public List<Product>? Products { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Blog>? Blogs { get; set; }
        public BlogSearchResultVM()
        {
            Blogs = new List<Blog>();
            Products = new List<Product>();
            Categories = new List<Category>();
        }
    }
}
