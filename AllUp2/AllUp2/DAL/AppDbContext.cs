using AllUp2.Models;
using Microsoft.EntityFrameworkCore;
using System.Composition;
using System.Reflection.Metadata;
using System;

namespace AllUp2.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions option) : base(option)
        {

        }
        //public DbSet<SliderContent> SliderContents { get; set; }
        //public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        //public DbSet<Bio> Bios { get; set; }
        //public DbSet<Blog> Blogs { get; set; }
        //public DbSet<Expert> Experts { get; set; }
        //public DbSet<Testimonial> Testimonials { get; set; }
        //public DbSet<InstagramPost> InstagramPosts { get; set; }
        //public DbSet<Book> Books { get; set; }
        //public DbSet<Ganre> Ganres { get; set; }
        //public DbSet<BookGanre> BookGanres { get; set; }





       
            

        }
    }
