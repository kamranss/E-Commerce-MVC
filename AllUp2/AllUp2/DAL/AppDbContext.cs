using AllUp2.Models;
using Microsoft.EntityFrameworkCore;
using System.Composition;
using System.Reflection.Metadata;
using System;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;

namespace AllUp2.DAL
{
    public class AppDbContext : DbContext
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
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<SocialPlatform> SocialPlatforms { get; set; }
        public DbSet<About> Abouts { get; set; }

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

            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Siemens" },
                new Brand { Id = 2, Name = "Samsung" },
                new Brand { Id = 3, Name = "Dell" },
                new Brand { Id = 4, Name = "Apple" },
                new Brand { Id = 5, Name = "Xiomi" },
                new Brand { Id = 6, Name = "Toshiba" },
                new Brand { Id = 7, Name = "Nokia" },
                new Brand { Id = 8, Name = "BackBerry" },
                new Brand { Id = 9, Name = "Hp" },
                new Brand { Id = 10, Name = "Asus" },
                new Brand { Id = 11, Name = "Lenova" }
              );
            modelBuilder.Entity<Color>().HasData(
                new Color { Id = 1, Name = "White" },
                new Color { Id = 2, Name = "Red" },
                new Color { Id = 3, Name = "Yellow" },
                new Color { Id = 4, Name = "Black" },
                new Color { Id = 5, Name = "Orange" },
                new Color { Id = 6, Name = "Grey" },
                new Color { Id = 7, Name = "Green" },
                new Color { Id = 8, Name = "Blue" },
                new Color { Id = 9, Name = "Purple" }
              );
            modelBuilder.Entity<Size>().HasData(
               new Size { Id = 1, Name = "XS" },
               new Size { Id = 2, Name = "S" },
               new Size { Id = 3, Name = "M" },
               new Size { Id = 4, Name = "L" },
               new Size { Id = 5, Name = "XL" }
             );
            modelBuilder.Entity<About>().HasData(
             new About { Id = 1, Name = "Our Company", Description = "Lorem ipsum dolor sit amet conse ctetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam. Lorem ipsum dolor sit amet conse ctetur adipisicing elit."},
             new About { Id = 2, Name = "S", Description = "Lorem ipsum dolor sit amet conse ctetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam. Lorem ipsum dolor sit amet conse ctetur adipisicing elit." },
             new About { Id = 3, Name = "M", Description = "Lorem ipsum dolor sit amet conse ctetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam. Lorem ipsum dolor sit amet conse ctetur adipisicing elit." },
             new About { Id = 4, Name = "L", Description = "Lorem ipsum dolor sit amet conse ctetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam. Lorem ipsum dolor sit amet conse ctetur adipisicing elit." },
             new About { Id = 5, Name = "XL", Description = "Lorem ipsum dolor sit amet conse ctetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam. Lorem ipsum dolor sit amet conse ctetur adipisicing elit." }
           );


        }
    }
}


//modelBuilder.Entity<Category>()
//          .HasMany(c => c.SubCategories)
//          .WithOne(c => c.ParentCategory)
//          .HasForeignKey(c => c.ParentCategoryId);
////base.OnModelCreating(modelBuilder);