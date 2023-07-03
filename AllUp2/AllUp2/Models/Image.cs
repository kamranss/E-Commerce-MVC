namespace AllUp2.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsMain { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public bool? IsSliderImage { get; set; }
        public bool? IsBannerImage { get; set; }

    }
}
