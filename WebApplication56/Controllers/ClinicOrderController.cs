using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication56.Models;

namespace WebApplication56.Controllers
{
    public class ClinicOrderController : Controller
    {
        private readonly doctorContext _context;

        public ClinicOrderController(doctorContext context)
        {
            _context = context;
        }

        // GET: ClinicOrder
        public async Task<IActionResult> Index()
        {
            var doctorContext = _context.Orders.Include(o => o.ClinicNameNavigation).Include(o => o.EmailNavigation);
            return View(await doctorContext.ToListAsync());
        }

        // GET: ClinicOrder/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.ClinicNameNavigation)
                .Include(o => o.EmailNavigation)
                .FirstOrDefaultAsync(m => m.OrdId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: ClinicOrder/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ClinicName"] = new SelectList(_context.Clinics, "ClinicName", "ClinicName", order.ClinicName);
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email", order.Email);
            return View(order);
        }

        // POST: ClinicOrder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrdId,OrdDate,ClinicName,Email,Type,Status")] Order order)
        {
            if (id != order.OrdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrdId))
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
            ViewData["ClinicName"] = new SelectList(_context.Clinics, "ClinicName", "ClinicName", order.ClinicName);
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email", order.Email);
            return View(order);
        }

        // GET: ClinicOrder/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.ClinicNameNavigation)
                .Include(o => o.EmailNavigation)
                .FirstOrDefaultAsync(m => m.OrdId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: ClinicOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'doctorContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(string id)
        {
            return (_context.Orders?.Any(e => e.OrdId == id)).GetValueOrDefault();
        }


        // Other controller actions...

        // GET: orders/Create
        public IActionResult Create(string clinicName)
        {
            string selectedClinic = HttpContext.Session.GetString("SelectedClinic");
            ViewBag.SelectedClinic = selectedClinic;
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email");

            // Get the email of the logged-in user
            string userEmail = HttpContext.Session.GetString("UserEmail");

            // Pass the email to the view
            ViewBag.UserEmail = userEmail;
            // Generate OrdId
            string ordId = GenerateAutoIncrementIdForSales();

            // Pass generated OrdId to the view
            ViewBag.OrdId = ordId;

            // Pass selected clinic name to the view
            if (selectedClinic != null)
            {
                ViewBag.SelectedClinic = selectedClinic;
            }
            return View();
        }




        [HttpGet]
        public IActionResult GetLatestOrderID()
        {
            // Query the database to get the orders starting with "s"
            var ordersStartingWiths = _context.Orders
                .Where(o => o.OrdId.StartsWith("s"))
                .Select(o => o.OrdId); // Select the order IDs

            // Extract the numeric part of each order ID and parse it to integers
            var numericParts = ordersStartingWiths
                .AsEnumerable() // Switch to client evaluation
                .Select(id => int.TryParse(id.Substring(1), out int result) ? result : 0);

            // Get the maximum numeric part and increment it by 1
            int nextOrderNumber = numericParts.Any() ? numericParts.Max() + 1 : 1;

            // Construct the next order ID with the prefix "p" and the incremented numeric part
            string nextOrderID = "s" + nextOrderNumber;

            // Return the next order ID
            return Ok(nextOrderID);
        }




        // Helper methods for generating order IDs
        private string GenerateAutoIncrementIdForSales()
        {
            int count = _context.Orders.Count(o => o.Type == "توريد");
            int nextId = count + 1;
            return "s" + nextId.ToString();
        }



        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdDate,ClinicName,Email,Type,Status")] Order order)
        {

            string selectedClinic = HttpContext.Session.GetString("SelectedClinic");
            ViewBag.SelectedClinic = selectedClinic;

            // Set the ClinicName property of the order object
            order.ClinicName = selectedClinic;

            order.Email = HttpContext.Session.GetString("UserEmail");
            if (ModelState.IsValid)
            {
                // Generate auto-increment ID for شراء
                order.OrdId = GenerateAutoIncrementIdForSales();

                // Ensure OrdId is not null before adding the order to the context
                if (!string.IsNullOrEmpty(order.OrdId))
                {
                    try
                    {
                        // Add the order to the context
                        _context.Add(order);
                        await _context.SaveChangesAsync();

                        // Save the generated OrdId in session
                        HttpContext.Session.SetString("GeneratedOrdId", order.OrdId);

                        // Redirect to another action within this controller
                        return RedirectToAction("Index", "Tools");
                    }
                    catch (Exception ex)
                    {
                        // Log the detailed error message
                        Console.WriteLine($"An error occurred while saving order: {ex.Message}");
                        ModelState.AddModelError(string.Empty, "Error occurred while saving order.");
                        return View(order); // Return the view with the order object to display the error message
                    }
                }
                else
                {
                    // If OrdId is null, there's an issue with generation
                    Console.WriteLine("OrdId is null"); // Debug line
                    ModelState.AddModelError("OrdId", "Failed to generate Order ID.");
                }
            }

            ViewData["ClinicName"] = new SelectList(_context.Clinics, "ClinicName", "ClinicName", order.ClinicName);
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email", order.Email);
            return View(order);
        }





    }
}
