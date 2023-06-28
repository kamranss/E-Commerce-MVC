using System.ComponentModel.DataAnnotations.Schema;

namespace AllUp2.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(Category.Id))]
        public int? ParentCategoryId { get; set; }
        public List<Category> SubCategories { get; set; }
        // one category having multiple products thats why main side keeps the list of the entities
        public List<Product>? Products { get; set; }
    }
}
