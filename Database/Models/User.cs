using System.ComponentModel.DataAnnotations;

namespace Air_3550.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string LoginId { get; set; }
        [Required]
        public string HashedPass { get; set; }
        [Required]
        public Role UserRole { get; set; }
        public CustomerInfo CustInfo { get; set; }
    }
}
