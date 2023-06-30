using AllUp2.DAL;
using AllUp2.Helper;
using AllUp2.Models;
using AllUp2.Services.BasketS;
using AllUp2.Services.EmailService;
using AllUp2.Services.FileService;
using AllUp2.Services.Home;
using AllUp2.Services.OTPService;
using AllUp2.Services.ProductS;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace AllUp2
{
    public static class ServiceRegistration
    {
        public static void ServicesRegister(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            //services.AddMvc();
            services.AddMemoryCache();


           /* services.AddScoped<Account>(s => new Account("random"));*/ // creaitng instance from custom Class -- alsp here we are sending data to constructor
            //Configuration of session and setting time for session lifespan
            //services.AddSession(option =>
            //{
            //    option.IdleTimeout = TimeSpan.FromMinutes(10);
            //});
            //services.AddHttpContextAccessor();
            //services.AddScoped<IBasketService, BasketService>();
            //services.AddIdentity<AppUser, IdentityRole>(options =>
            //{
            //    options.Password.RequiredLength = 8;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireDigit = true;
            //    options.SignIn.RequireConfirmedEmail = true;

            //    options.User.RequireUniqueEmail = true;
            //    options.Lockout.AllowedForNewUsers = true;

            //    // this is mainly controlling user attemps and locking for some period if something goes wrong -- mainly used together
            //    options.Lockout.MaxFailedAccessAttempts = 3;
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            //})
            //.AddEntityFrameworkStores<AppDbContext>()
            //.AddDefaultTokenProviders() // this basicly serves for token generation
            //.AddErrorDescriber<CustomidentityErrorDescriber>(); // this is serving for get error descriptions which we indicated within helper 
            //services.AddSignalR();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IProductService, ProductService>(); // using this approach we are asking from Program class service that create instance for us from IProduct interface and return ProductService
            services.AddScoped<IEmailService, EmailService>(); // injecting our service within IO container
            services.AddScoped<IFileService, FileService>(); // injecting our service within IO container
            services.AddScoped<IBasketService, BasketService>();
            //services.AddScoped<OtpService>(); // generate otp service

        }
    }
}
