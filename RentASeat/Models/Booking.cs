using System;
using System.ComponentModel.DataAnnotations;

namespace RentASeat.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int PickupLocationId { get; set; }

        public int? DropOffLocationId { get; set; }

        [Required]
        public DateTime PickupDate { get; set; }

        [Required]
        public DateTime DropOffDate { get; set; }

        public Location PickupLocation { get; set; }

        public Location DropOffLocation { get; set; }
    }
}