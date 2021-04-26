using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class Plane
    {
        public int PlaneId { get; set; }
        [Required]
        public string Model { get; set; }
        public int MaxSeats { get; set; }
    }
}
