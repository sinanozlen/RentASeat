using System.ComponentModel.DataAnnotations;

namespace RentASeat.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
