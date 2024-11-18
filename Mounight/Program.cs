using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mounight.Areas.Identity.Data;
using Mounight.Data;
using SimpleImageGallery.Data;
using SimpleImageGallery.Services;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthMounightContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthMounightContextConnection' not found.");

builder.Services.AddDbContext<AuthMounightContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<SimpleImageGalleryDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<AuthUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AuthMounightContext>();
builder.Services.AddScoped<IImage, ImageService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Gallery}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
