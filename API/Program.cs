using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System.Reflection;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<RenASeatContext>();


//Brand Ýçin Gerekli olan 
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
