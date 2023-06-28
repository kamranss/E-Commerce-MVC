using AllUp2.Models;
using Microsoft.EntityFrameworkCore;
using System.Composition;
using System.Reflection.Metadata;
using System;
using Microsoft.AspNetCore.Identity;

namespace AllUp2.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions option) : base(option)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Bio> Bios { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<SocialPlatform> SocialPlatforms { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<AppUser>().HasData(
           new AppUser()
           {

               UserName = "Admin",
               FullName = "AdminAdmin",
               Email = "admin@gmail.com",
               EmailConfirmed = true,
               PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Admin12345!")

           }
             );
            //base.OnModelCreating(modelBuilder);
        }

    }
}
