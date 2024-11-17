using Microsoft.AspNetCore.Mvc;
using HasMohProject.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System.Threading.Tasks;

namespace HasMohProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Account/Register (Render the registration page)
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register (Handle the registration logic)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return View();
            }

            if (ModelState.IsValid)
            {
                // Check if the email already exists in the database
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("", "Email is already taken.");
                    return View();
                }

                // Hash the password before saving
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                user.CreatedAt = DateTime.Now;

                // Add user to the database
                _context.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Account"); // Redirect to login page
            }

            return View(user); // If validation fails, show the registration form again
        }

        // GET: Account/Login (Render the login page)
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login (Handle the login logic)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }

            // Optionally, log the user in (e.g., create a session or use ASP.NET Identity)
            return RedirectToAction("Index", "Home"); // Redirect to home page on successful login
        }
    }
}
