namespace AllUp2.ViewModels.Home
{
    public class BasketVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public int CategoryId { get; set; }
        public int BasketCount { get; set; }
        public string ImagesUrl { get; set; }
    }
}
