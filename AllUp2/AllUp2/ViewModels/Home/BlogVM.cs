using AllUp2.Models;

namespace AllUp2.ViewModels.Home
{
    public class BlogVM
    {
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Blog> Blogs { get; set; }
        public Blog Blog { get; set; }
        public Comment Comment { get; set; }
    }
}
