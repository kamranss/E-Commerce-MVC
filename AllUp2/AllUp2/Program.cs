using AllUp2;
using AllUp2.DAL;
using AllUp2.Hubs;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Math.EC.ECCurve;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.ServicesRegister();
var _config = builder.Configuration;

// Add services to the container.
//builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDbContext>(options =>  // here we are creating instance from our Dbcontext class whenever project will be run 
{
    options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<ChatHub>("/chat"); // fi after requst you will see -- Connection Id Required-- it means that you are already connected
app.Run();
