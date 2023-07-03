namespace AllUp2.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public int? ExpertId { get; set; }
        public string Content { get; set; }
        public Expert expert { get; set; }
    }
}
