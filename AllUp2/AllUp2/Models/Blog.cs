namespace AllUp2.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public bool IsVideo { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public BlogDetail? BlogDetail { get; set; }
        public List<Comment>? Comments { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
