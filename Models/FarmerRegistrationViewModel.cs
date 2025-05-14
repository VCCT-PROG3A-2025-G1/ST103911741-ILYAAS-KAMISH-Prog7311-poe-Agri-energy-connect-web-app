namespace Prog7311_poe.Models
{
    public class FarmerRegistrationViewModel
    {
        // Farmer properties
        public Farmer Farmer { get; set; } = new Farmer();

        // User properties
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
