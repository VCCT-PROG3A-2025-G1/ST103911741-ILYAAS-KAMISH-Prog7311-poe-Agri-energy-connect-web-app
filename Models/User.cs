using System.ComponentModel.DataAnnotations;

namespace Prog7311_poe.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; } = string.Empty; // "Farmer" or "Employee"

        // Foreign key for optional Farmer association
        public int? FarmerId { get; set; }

        // Optional navigation property
        public Farmer? Farmer { get; set; }
    }
}
