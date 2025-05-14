using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prog7311_poe.Models;
using System.Diagnostics;

namespace Prog7311_poe.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FarmerController(Prog7311_poe.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get current username
            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("No username found in Identity");
                return RedirectToAction("Login", "Account");
            }

            // Find the user first
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Role == "Farmer");

            if (user == null || !user.FarmerId.HasValue)
            {
                Console.WriteLine($"No farmer found for username: {username}");
                TempData["ErrorMessage"] = "Could not find your farmer profile.";
                return RedirectToAction("Index", "Home");
            }

            Console.WriteLine($"Found user with FarmerId: {user.FarmerId}");

            // Get the farmer with products
            var farmer = await _context.Farmers
                .Include(f => f.Products)
                .FirstOrDefaultAsync(f => f.FarmerId == user.FarmerId.Value);

            if (farmer == null)
            {
                Console.WriteLine($"No farmer found with ID: {user.FarmerId}");
                return NotFound();
            }

            // Debug information
            if (farmer.Products != null)
            {
                Console.WriteLine($"Found {farmer.Products.Count} products for farmer {farmer.Name}");
            }
            else
            {
                Console.WriteLine("Products collection is null");
            }

            return View(farmer);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(Product product)
        {
            // Get currently logged-in username
            var username = User.Identity?.Name;
            Console.WriteLine($"Current username: {username}");

            // Try to get FarmerId from claims first (most reliable)
            var farmerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "FarmerId");

            if (farmerIdClaim != null && int.TryParse(farmerIdClaim.Value, out int farmerId))
            {
                Console.WriteLine($"Found FarmerId in claims: {farmerId}");
                product.FarmerId = farmerId;
            }
            else
            {
                // Fallback: find the user and get FarmerId from there
                Console.WriteLine("FarmerId not found in claims, looking up user in database");
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == username && u.Role == "Farmer");

                if (user == null)
                {
                    Console.WriteLine("User not found or not a farmer");
                    TempData["ErrorMessage"] = "You must be logged in as a farmer to add products.";
                    return RedirectToAction("Index", "Home");
                }

                if (!user.FarmerId.HasValue)
                {
                    Console.WriteLine("User has no FarmerId");
                    TempData["ErrorMessage"] = "Your account is not properly linked to a farm profile.";
                    return RedirectToAction("Index", "Home");
                }

                Console.WriteLine($"Found user with FarmerId: {user.FarmerId}");
                product.FarmerId = user.FarmerId.Value;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Log product details before saving
                    Console.WriteLine($"Adding product: {product.Name}, Category: {product.Category}, Date: {product.ProductionDate}, FarmerId: {product.FarmerId}");

                    _context.Products.Add(product);
                    var result = await _context.SaveChangesAsync();
                    Console.WriteLine($"SaveChangesAsync returned: {result}");

                    TempData["SuccessMessage"] = $"Product '{product.Name}' was added successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in AddProduct: {ex.Message}");
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");

                    ModelState.AddModelError("", $"Error saving product: {ex.Message}");
                    TempData["ErrorMessage"] = "Failed to add product due to an error.";
                }
            }
            else
            {
                // Log model validation errors
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"Model validation error: {error.ErrorMessage}");
                    }
                }
            }

            return View(product);
        }
    }
}


