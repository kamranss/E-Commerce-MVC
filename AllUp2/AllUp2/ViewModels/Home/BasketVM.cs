﻿namespace AllUp2.ViewModels.Home
{
    public class BasketVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int? Count { get; set; }
        public int? CategoryId { get; set; }
        public int? BasketCount { get; set; }
    }
}
