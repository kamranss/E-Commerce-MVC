namespace AllUp2.Models
{
    public class Expert
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public string ImageUrl { get; set; }
        public List<Testimonial> Testimonials { get; set; }
    }
}
