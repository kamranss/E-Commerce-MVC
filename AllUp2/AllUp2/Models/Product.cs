namespace AllUp2.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int? Count { get; set; }
        public string? ProductCode { get; set; }
        public bool? IStock { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsSpecial { get; set; }
        public string? Description { get; set; }
        public int? Rate { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool? isNewArrival { get; set; }
        public bool? IsBsetSeller { get; set; }
        public bool? IsFeatured { get; set; }
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public int? ColorId { get; set; }
        public Color Color { get; set; }
        public int? CompositionId { get; set; }
        public Composition Composition { get; set; }
        public int? SizeId { get; set; }
        public Size Size { get; set; }
       
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Image> Images { get; set; }

        public Product()
        {
            Images = new List<Image>();

        }
    }
}
