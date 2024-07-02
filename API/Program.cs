using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Tools;
using EntitityLayer.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
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
//CarDescription  Ýçin Gerekli olan 
builder.Services.AddScoped<ICarDescriptionService, CarDescriptionManager>();
builder.Services.AddScoped<ICarDescriptionDal, EfCarDescriptionDal>();

//Reservation icin
builder.Services.AddScoped<IReservationService, ReservationManager>();
builder.Services.AddScoped<IReservationDal, EfReservationDal>();

//About
builder.Services.AddScoped<IAboutService, AboutManager>();
builder.Services.AddScoped<IAboutDal, EfAboutDal>();
//Reviews
builder.Services.AddScoped<IReviewService, ReviewManager>();
builder.Services.AddScoped<IReviewDal, EfReviewDal>();

//Pricing
builder.Services.AddScoped<IPricingService, PricingManager>();
builder.Services.AddScoped<IPricingDal, EfPricingDal>();

//Feature Ýçin Gerekli olan 
builder.Services.AddScoped<IFeatureService, FeatureManager>();
builder.Services.AddScoped<IFeatureDal, EfFeatureDal>();

//Service Ýçin Gerekli olan 
builder.Services.AddScoped<IServiceService, ServiceManager>();
builder.Services.AddScoped<IServiceDal, EfServiceDal>();

//Social Mediya Gerekli olan 
builder.Services.AddScoped<ISocialMediaService, SocialMediaManager>();
builder.Services.AddScoped<ISocialMediaDal, EfSocialMediaDal>();

//Banner Ýçin Gerekli olan 
builder.Services.AddScoped<IBannerService, BannerManager>();
builder.Services.AddScoped<IBannerDal, EfBannerDal>();
//AppUser  Ýçin Gerekli olan
builder.Services.AddScoped<IAppUserService, AppUserManager>();
builder.Services.AddScoped<IAppUserDal, EfAppUserDal>();
//Testimonial Ýçin Gerekli olan 
builder.Services.AddScoped<ITestimonialService, TestimonialManager>();
builder.Services.AddScoped<ITestimonialDal, EfTestimonialDal>();
//Contact Ýçin Gerekli olan 
builder.Services.AddScoped<IContactService, ContactManager>();
builder.Services.AddScoped<IContactDal, EfContactDal>();
//FooterAddress Ýçin Gerekli olan 
builder.Services.AddScoped<IFooterAddressService, FooterAddressManager>();
builder.Services.AddScoped<IFooterAddressDal, EfFooterAddressDal>();
//Location Ýçin Gerekli olan 
builder.Services.AddScoped<ILocationService, LocationManager>();
builder.Services.AddScoped<ILocationDal, EfLocationDal>();
//Pricing Ýçin Gerekli olan 
builder.Services.AddScoped<IPricingService, PricingManager>();
builder.Services.AddScoped<IPricingDal, EfPricingDal>();
//CarPricing Ýçin Gerekli olan 
builder.Services.AddScoped<ICarPricingService, CarPricingManager>();
builder.Services.AddScoped<ICarPricingDal, EfCarPricingDal>();
//RentACar Ýçin Gerekli olan 
builder.Services.AddScoped<IRentACarDal, EfRentACarDal>();
builder.Services.AddScoped<IRentACarService, RentACarManager>();
//CarFeature Ýçin Gerekli olan 
builder.Services.AddScoped<ICarFeatureDal, EfCarFeatureDal>();
builder.Services.AddScoped<ICarFeatureService, CarFeatureManager>();


// Car için gerekli olan
builder.Services.AddScoped<ICarService, CarManager>();
builder.Services.AddScoped<ICarDal, EfCarDal>();

// Feature için gerekli olan
builder.Services.AddScoped<IFeatureService, FeatureManager>();
builder.Services.AddScoped<IFeatureDal, EfFeatureDal>();

// Location için gerekli olan
builder.Services.AddScoped<ILocationService, LocationManager>();
builder.Services.AddScoped<ILocationDal, EfLocationDal>();

// Pricing için gerekli olan
builder.Services.AddScoped<IPricingService, PricingManager>();
builder.Services.AddScoped<IPricingDal, EfPricingDal>();

// Add user service
builder.Services.AddSingleton<UserService>();
builder.Services.AddControllers().AddFluentValidation(x =>
{
    x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidAudience = JwtTokenDefaults.ValidAudience,
        ValidIssuer = JwtTokenDefaults.ValidIssuer,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
builder.Services.AddControllers();

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

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
