using System.ComponentModel.DataAnnotations.Schema;

namespace AllUp2.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsMain { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreationDate { get; set; }
        //public int? ImageId { get; set; }
        //public Image Image { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public List<Category> SubCategories { get; set; }
        public List<Image> Images { get; set; }
        // one category having multiple products thats why main side keeps the list of the entities
        public List<Product>? Products { get; set; }
        public List<Blog>? Blogs { get; set; }
    }
}
