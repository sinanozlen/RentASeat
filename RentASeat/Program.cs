using Microsoft.EntityFrameworkCore;
using RentASeat.Data;

var builder = WebApplication.CreateBuilder(args);

// Output connection string to verify it's loading correctly
Console.WriteLine("Connection String: " + builder.Configuration.GetConnectionString("DefaultConnection"));

// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext hizmetini ekleyin
builder.Services.AddDbContext<RentASeatContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "Customer",
    pattern: "{area:exists}/{controller=Customer}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "Manager",
    pattern: "{area:exists}/{controller=Manager}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
