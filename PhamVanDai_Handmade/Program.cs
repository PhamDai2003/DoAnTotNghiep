using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models;
using PhamVanDai_Handmade.Repository;
using PhamVanDai_Handmade.Repository.Services.OpenStreetMap;

var builder = WebApplication.CreateBuilder(args);

// add Identity
builder.Services.AddIdentity<UserModel, RoleModel>(options =>
{
    //password setting
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ??ng ký OpenStreetMapService dùng HttpClient
builder.Services.AddHttpClient<OpenStreetMapService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


