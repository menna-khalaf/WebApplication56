using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication56.Models;

namespace WebApplication56.Controllers
{
    public class UsersController : Controller
    {
        private readonly doctorContext _context;

        public UsersController(doctorContext context)
        {
            _context = context;
        }


        public IActionResult navbar()
        {
            return View();
        }



        // GET: Users
        public async Task<IActionResult> Index()
        {
            // Retrieve the email of the authenticated user from session
            var userEmail = HttpContext.Session.GetString("UserEmail");

            // Check if userEmail is null or empty
            if (string.IsNullOrEmpty(userEmail))
            {
                // Handle the case where userEmail is null or empty
                return RedirectToAction("Login", "Users"); // Redirect to login page or handle appropriately
            }

            // Retrieve the user from the database based on the email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            // Check if the user exists
            if (user != null)
            {
                // User exists, return the view with the user data
                return View(new List<User> { user });
            }
            else
            {
                // If the user does not exist, handle the error appropriately
                return Problem("User not found in the database.");
            }
        }


        // GET: Users/Create
        public IActionResult Update()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Update(User userModel)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(userModel.Email))
            {
                // Hash the provided password for comparison
                string hashedPassword = HashPassword(userModel.Password);

                // Check if the user exists and the hashed passwords match
                var user = _context.Users.FirstOrDefault(u => u.Email == userModel.Email && u.Password == hashedPassword && u.Position == userModel.Position);

                if (user != null)
                {
                    // Successfully authenticated
                    // Store user's email in session
                    HttpContext.Session.SetString("UserEmail", user.Email);

                    // Redirect to the Index action to display user data
                    return RedirectToAction("Index");
                }
            }

            // Authentication failed, remain on the same page
            return View();
        }




        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Email == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Password,Position")] User user)
        {
            if (ModelState.IsValid)
            {
                // Hash the password before storing it
                user.Password = HashPassword(user.Password);

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }


        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Don't include the password in the model passed to the view
            var userModel = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Position = user.Position
            };

            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName,Email,Password,Position")] User user)
        {
            if (id != user.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _context.Users.FindAsync(id);

                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    // Update fields
                    existingUser.FirstName = user.FirstName;
                    existingUser.LastName = user.LastName;
                    existingUser.Position = user.Position;

                    // If a new password is provided, hash it; otherwise, keep the existing password
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        existingUser.Password = HashPassword(user.Password);
                    }

                    _context.Update(existingUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Email))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        //private bool UserExists(string id)
        //{
        //    return _context.Users.Any(e => e.Email == id);
        //}



        // GET: Users/Delete
        public async Task<IActionResult> Delete()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Users");
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email == userEmail && m.IsActive);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        
        public async Task<IActionResult> DeleteUser()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                return Json(new { success = false, message = "User is not logged in." });
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email == userEmail && m.IsActive);
            if (user != null)
            {
                user.IsActive = false; // Update IsActive property to false
                _context.Update(user);
                await _context.SaveChangesAsync();

                HttpContext.Session.Clear();

                return Json(new { success = true, message = "Your account has been successfully deleted." });
            }

            return Json(new { success = false, message = "User not found." });
        }



        private bool UserExists(string id)
        {
            return (_context.Users?.Any(e => e.Email == id)).GetValueOrDefault();
        }





        [HttpGet]
        [ActionName("login")]
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        [ActionName("login")]
        public IActionResult Login(User userModel)
        {
            if (ModelState.IsValid)
            {
                // Hash the provided password for comparison
                string hashedPassword = HashPassword(userModel.Password);

                // Check if the user exists, the hashed passwords match, and the user is active
                var user = _context.Users.FirstOrDefault(u => u.Email == userModel.Email && u.Password == hashedPassword && u.Position == userModel.Position && u.IsActive);

                if (user != null)
                {
                    // Successfully logged in
                    // Store user's email in session
                    HttpContext.Session.SetString("UserEmail", user.Email);

                    // Redirect based on user's position
                    if (user.Position == "مدير المستشفي")
                    {
                        return RedirectToAction("Index", "Report");
                    }
                    else if (user.Position == "امين المخزن")
                    {
                        return RedirectToAction("Create", "Adjustments");
                    }
                    else if (user.Position == "التمريض")
                    {
                        return RedirectToAction("Index", "Clinic");
                    }
                    else if (user.Position == "مدير المخزن")
                    {
                        return RedirectToAction("Index", "OrderedTools");
                    }
                    else if (user.Position == "كاتب الشطب")
                    {
                        return RedirectToAction("Create", "Orders");
                    }
                    // Add more conditions as needed for other positions
                }
            }

            // Login failed
            ViewBag.LoginStatus = 0;
            return View("Login");
        }
        private string HashPassword(string password)
        {
            // Hash the password using SHA-256 algorithm
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Compute hash from password
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
