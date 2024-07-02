using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using AspNet.Security.OAuth.Twitter;
using AspNet.Security.OAuth.Instagram;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Twitter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie
    (JwtBearerDefaults.AuthenticationScheme, opt =>

    {

        opt.LoginPath = "/Login/Index";
        opt.LoginPath = "/Login/LogOut/";
        opt.AccessDeniedPath = "/Pages/AccessDenied/";
        opt.Cookie.SameSite = SameSiteMode.Strict;
        opt.Cookie.HttpOnly = true;
        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        opt.Cookie.Name = "CarBookJwt";

    });

builder.Services.AddRazorPages();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    IConfigurationSection googleAuthNSection =
        builder.Configuration.GetSection("Authentication:Google");
    options.ClientId = googleAuthNSection["ClientId"];
    options.ClientSecret = googleAuthNSection["ClientSecret"];
})
.AddTwitter(TwitterDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration["Authentication:Twitter:ConsumerKey"];
    options.ClientSecret = builder.Configuration["Authentication:Twitter:ConsumerSecret"];
});
//.AddInstagram(InstagramDefaults.AuthenticationScheme, options =>
//{
//    options.ClientId = builder.Configuration["Authentication:Instagram:ClientId"];
//    options.ClientSecret = builder.Configuration["Authentication:Instagram:ClientSecret"];
//});

// CORS politikasýný burada ekliyoruz
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.WithOrigins("https://example.com", "https://another-example.com")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials(); // Gerekirse credentials (kimlik bilgileri) desteðini etkinleþtirin
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// CORS politikasýný ekliyoruz
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
});

app.Run();
