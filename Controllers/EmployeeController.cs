using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Prog7311_poe.Models;
using Microsoft.EntityFrameworkCore;

namespace Prog7311_poe.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get statistics for the dashboard
            ViewBag.FarmerCount = await _context.Farmers.CountAsync();
            ViewBag.ProductCount = await _context.Products.CountAsync();
            ViewBag.CategoryCount = await _context.Products.Select(p => p.Category).Distinct().CountAsync();

            return View();
        }

        public IActionResult AddFarmer()
        {
            return View(new FarmerRegistrationViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFarmer(FarmerRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userExists = await _context.Users.AnyAsync(u => u.Username == model.Username);
                if (userExists)
                {
                    ModelState.AddModelError("Username", "Username already exists. Please choose a different one.");
                    return View(model);
                }

                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Passwords do not match");
                    return View(model);
                }

                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // First add the farmer
                    _context.Add(model.Farmer);
                    await _context.SaveChangesAsync();

                    // Log the new farmer's ID
                    Console.WriteLine($"Created new farmer with ID: {model.Farmer.FarmerId}");

                    // Create user with the farmerId
                    var user = new User
                    {
                        Username = model.Username,
                        Password = model.Password,
                        Role = "Farmer",
                        FarmerId = model.Farmer.FarmerId
                    };

                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    // Log the new user
                    Console.WriteLine($"Created new user with ID: {user.UserId}, Username: {user.Username}, Role: {user.Role}, FarmerId: {user.FarmerId}");

                    TempData["SuccessMessage"] = "Farmer added successfully with login credentials!";
                    return RedirectToAction(nameof(ViewFarmers));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error in AddFarmer: {ex.Message}");
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");
                    TempData["ErrorMessage"] = "An error occurred while adding the farmer.";
                    ModelState.AddModelError(string.Empty, $"Error adding farmer: {ex.Message}");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ViewFarmers()
        {
            var farmers = await _context.Farmers.ToListAsync();
            return View(farmers);
        }

        public async Task<IActionResult> ViewProducts(int? farmerId, string category, DateTime? startDate, DateTime? endDate)
        {
            ViewData["FarmerId"] = new SelectList(_context.Farmers, "FarmerId", "Name", farmerId);
            ViewData["Categories"] = new SelectList(_context.Products.Select(p => p.Category).Distinct());

            var viewModel = new ProductFilterViewModel
            {
                FarmerId = farmerId,
                Category = category,
                StartDate = startDate,
                EndDate = endDate
            };

            var productsQuery = _context.Products.Include(p => p.Farmer).AsQueryable();

            if (farmerId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.FarmerId == farmerId.Value);
            }

            if (!string.IsNullOrEmpty(category))
            {
                productsQuery = productsQuery.Where(p => p.Category == category);
            }

            if (startDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate <= endDate.Value);
            }

            viewModel.Products = await productsQuery.ToListAsync();

            return View(viewModel);
        }
    }
}