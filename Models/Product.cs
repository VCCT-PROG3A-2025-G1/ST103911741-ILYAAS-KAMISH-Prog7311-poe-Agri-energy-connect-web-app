using System.ComponentModel.DataAnnotations;

namespace Prog7311_poe.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Production date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Production Date")]
        public DateTime ProductionDate { get; set; }

        // Foreign Key
        public int FarmerId { get; set; }

        // Navigation property
        public Farmer? Farmer { get; set; } 
    }
}
