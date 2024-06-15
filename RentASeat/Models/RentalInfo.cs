using System;
using System.ComponentModel.DataAnnotations;

namespace RentASeat.Models
{
    public class RentalInfo
    {
        [Required]
        [Display(Name = "Nereden Alacaksınız?")]
        public string PickupLocation { get; set; }

        [Required]
        [Display(Name = "Alış Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime PickupDate { get; set; }

        [Required]
        [Display(Name = "Bırakma Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime ReturnDate { get; set; }

        [Required]
        [Display(Name = "Sürücü Yaşı")]
        public string DriverAge { get; set; }
    }
}
