using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Prog7311_poe.Models;
using Microsoft.EntityFrameworkCore;

namespace Prog7311_poe.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Use Include to explicitly load the related Farmer entity
                    var user = await _context.Users
                        .Include(u => u.Farmer)  // Include the related Farmer entity
                        .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

                    if (user != null)
                    {
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username ?? ""),
                    new Claim(ClaimTypes.Role, user.Role ?? "")
                };

                        // Make sure to always add FarmerId claim for Farmer users
                        if (user.Role == "Farmer" && user.FarmerId.HasValue)
                        {
                            claims.Add(new Claim("FarmerId", user.FarmerId.Value.ToString()));
                            // Log that we're adding the claim for debugging
                            Console.WriteLine($"Adding FarmerId claim: {user.FarmerId.Value}");
                        }

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties { IsPersistent = model.RememberMe };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

                        // Log successful login
                        Console.WriteLine($"User {user.Username} logged in successfully with role {user.Role}");
                        if (user.FarmerId.HasValue)
                        {
                            Console.WriteLine($"User has FarmerId: {user.FarmerId.Value}");
                        }

                        return user.Role == "Farmer"
                            ? RedirectToAction("Index", "Farmer")
                            : RedirectToAction("Index", "Employee");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Login exception: {ex.Message}");
                    TempData["ErrorMessage"] = "Login failed due to an internal error.";
                    ModelState.AddModelError(string.Empty, $"Unexpected error: {ex.Message}");
                }
            }

            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}