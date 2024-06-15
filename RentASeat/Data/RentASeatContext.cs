using Microsoft.EntityFrameworkCore;
using RentASeat.Models;

namespace RentASeat.Data
{
    public class RentASeatContext : DbContext
    {
        public RentASeatContext(DbContextOptions<RentASeatContext> options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
