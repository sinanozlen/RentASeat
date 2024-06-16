using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YourNamespace.Helpers;
using YourNamespace.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RenASeatContext>();

// Add brand services
builder.Services.AddScoped<IBrandService, BrandManager>();
builder.Services.AddScoped<IBrandDal, EfBrandDal>();
//Car Ýçin Gerekli olan 
builder.Services.AddScoped<ICarService, CarManager>();
builder.Services.AddScoped<ICarDal, EfCarDal>();

//Feature Ýçin Gerekli olan 
builder.Services.AddScoped<IFeatureService, FeaatureManager>();
builder.Services.AddScoped<IFeatureDal, EfFeatureDal>();
//Location Ýçin Gerekli olan 
builder.Services.AddScoped<ILocationService, LocationManager>();
builder.Services.AddScoped<ILocationDal, EfLocationDal>();
//Pricing Ýçin Gerekli olan 
builder.Services.AddScoped<IPricingService, PricingManager>();
builder.Services.AddScoped<IPricingDal, EfPricingDal>();

// Add user service
builder.Services.AddSingleton<UserService>();

// JWT Authentication
var key = Encoding.ASCII.GetBytes("your-256-bit-secret-your-256-bit-secret");
builder.Services.AddSingleton(new JwtAuthenticationManager(key));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
