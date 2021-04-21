using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
