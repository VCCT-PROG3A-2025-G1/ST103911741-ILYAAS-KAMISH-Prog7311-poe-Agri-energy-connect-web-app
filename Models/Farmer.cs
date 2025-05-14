using System.ComponentModel.DataAnnotations;

namespace Prog7311_poe.Models
{
    public class Farmer
    {
        public int FarmerId { get; set; }

        [Required(ErrorMessage = "Farmer name is required")]
        [Display(Name = "Farmer Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact number is required")]
        [Display(Name = "Contact Number")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string ContactNumber { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
