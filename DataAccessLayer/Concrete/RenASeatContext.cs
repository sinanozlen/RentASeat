using EntitityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class RenASeatContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=carbookdb.cpmew0w400zv.eu-north-1.rds.amazonaws.com,1433;Database=RentASeat;User Id=admin;Password=330457Fg;TrustServerCertificate=true;");
        }

        internal object Include(Func<object, object> value)
        {
            throw new NotImplementedException();
        }

        public DbSet<Car> Cars { get; set; }
        
        public DbSet<About> Abouts { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CarFeature> CarFeatures { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<CarDescription> CarDescriptions { get; set; }
        public DbSet<CarPricing> CarPricings { get; set; }
        public DbSet<Pricing> Pricings { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<FooterAddress> FooterAddresses { get; set; }
        public DbSet<RentACar> RentACar { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(x=>x.PickUpLocation)
                .WithMany(y=>y.PickUpReservation)
                .HasForeignKey(z=>z.PickUpLocationID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Reservation>()
                .HasOne(x => x.DropOffLocation)
                .WithMany(y => y.DropOffReservation)
                .HasForeignKey(z => z.DropOffLocationID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }




    }
}
