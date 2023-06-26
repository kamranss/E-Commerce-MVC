namespace Allup.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // one category having multiple products thats why main side keeps the list of the entities
        public List<Product>? Products { get; set; }
    }
}
