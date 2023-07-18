namespace AllUp2.Models
{
    public class BlogDetail
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public string ContentHeader { get; set; }
        public string ContentQuote { get; set; }
        public string ContentFooter { get; set; }
    }
}
