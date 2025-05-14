namespace Prog7311_poe.Models
{

    public class ProductFilterViewModel
    {
        public int? FarmerId { get; set; }
        public string? Category { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}

