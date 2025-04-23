using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication56.Models;

namespace WebApplication56.Controllers
{
    public class OrdersController : Controller
    {
        private readonly doctorContext _context;

        public OrdersController(doctorContext context)
        {
            _context = context;
        }

        // GET: Orders

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            // Retrieve the generatedOrdId from session
            var generatedOrdId = HttpContext.Session.GetString("GeneratedOrdId");

            // Retrieve orders based on the generatedOrdId
            var ordersWithGeneratedOrdId = await _context.Orders
                .Include(o => o.EmailNavigation)
                .Where(o => o.OrdId == generatedOrdId)
                .ToListAsync();

            // Retrieve tool names from the database
            var toolNames = await _context.Tools
                .Select(t => t.ToolName)
                .ToListAsync();

            // Pass the filtered orders and tool names to the view
            ViewData["ToolNames"] = toolNames;
            ViewBag.OrdId = generatedOrdId;

            return View(ordersWithGeneratedOrdId);
        }


        // GET: Orders/Details/5
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

        // GET: Orders/Create
        public IActionResult Create()
        {

            // Get the email of the logged-in user
            string userEmail = HttpContext.Session.GetString("UserEmail");

            // Pass the email to the view
            ViewBag.UserEmail = userEmail;

            ViewData["ClinicName"] = new SelectList(_context.Clinics, "ClinicName", "ClinicName");
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email");
            return View();
        }

        //private string GenerateAutoIncrementIdForPurchase()
        //{
        //    // Query the database to get the count of existing شراء orders
        //    int count = _context.Orders.Count(o => o.Type == "شراء");

        //    // Increment the count to generate the next ID
        //    int nextId = count + 1;

        //    // Format the ID with the prefix "p" and return
        //    return "p" + nextId.ToString();
        //}

        //private string GenerateAutoIncrementIdForSupply()
        //{
        //    // Query the database to get the count of existing توريد orders
        //    int count = _context.Orders.Count(o => o.Type == "توريد");

        //    // Increment the count to generate the next ID
        //    int nextId = count + 1;

        //    // Format the ID with the prefix "s" and return
        //    return "s" + nextId.ToString();
        //}

        [HttpGet]
        public IActionResult GetLatestOrderID()
        {
            // Query the database to get the orders starting with "p"
            var ordersStartingWithP = _context.Orders
                .Where(o => o.OrdId.StartsWith("p"))
                .Select(o => o.OrdId); // Select the order IDs

            // Extract the numeric part of each order ID and parse it to integers
            var numericParts = ordersStartingWithP
                .AsEnumerable() // Switch to client evaluation
                .Select(id => int.TryParse(id.Substring(1), out int result) ? result : 0);

            // Get the maximum numeric part and increment it by 1
            int nextOrderNumber = numericParts.Any() ? numericParts.Max() + 1 : 1;

            // Construct the next order ID with the prefix "p" and the incremented numeric part
            string nextOrderID = "p" + nextOrderNumber;

            // Return the next order ID
            return Ok(nextOrderID);
        }




        // Helper methods for generating order IDs
        private string GenerateAutoIncrementIdForPurchase()
        {
            int count = _context.Orders.Count(o => o.Type == "شراء");
            int nextId = count + 1;
            return "p" + nextId.ToString();
        }








        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdId,OrdDate,ClinicName,Email,Type,Status")] Order order)
        {

            order.Email = HttpContext.Session.GetString("UserEmail");
            if (ModelState.IsValid)
            {
                // Generate auto-increment ID for شراء
                order.OrdId = GenerateAutoIncrementIdForPurchase();

                // Ensure OrdId is not null before adding the order to the context
                if (!string.IsNullOrEmpty(order.OrdId) && !string.IsNullOrEmpty(order.Email))
                {
                    try
                    {
                        // Add the order to the context
                        _context.Add(order);
                        await _context.SaveChangesAsync();


                        HttpContext.Session.SetString("GeneratedOrdId", order.OrdId);
                        return RedirectToAction(nameof(Index));
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
                    //ModelState.AddModelError("OrdId", "Failed to generate Order ID.");
                    ModelState.AddModelError("Email", "you should login to system");


                }
            }

            ViewData["ClinicName"] = new SelectList(_context.Clinics, "ClinicName", "ClinicName", order.ClinicName);
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email", order.Email);
            return View(order);
        }
 

        // GET: Orders/Edit/5
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

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Orders/Delete/5
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

        // POST: Orders/Delete/5
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





        private List<string> GetAllToolNames()
        {
            return _context.Tools.Select(t => t.ToolName).ToList();
        }








        public class OrderData
        {
            public string OrderId { get; set; } // Keep OrderId as a string if it's stored as a string in the database
            public string ToolName { get; set; }
            public int Quantity { get; set; }
        }

        [HttpPost]
        public IActionResult Insertion([FromBody] List<OrderData> ordersData)
        {
            try
            {
                // Retrieve the order ID from the session
                var ordId = HttpContext.Session.GetString("GeneratedOrdId");

                if (string.IsNullOrEmpty(ordId))
                {
                    return BadRequest("No OrderId found in session.");
                }

                if (ordersData == null || !ordersData.Any())
                {
                    return BadRequest("No data received");
                }

                foreach (var orderData in ordersData)
                {
                    // Create a new OrderedTool object and set its properties
                    var newOrderedTool = new OrderedTool
                    {
                        OrdId = ordId, // Use the ordId retrieved from the session
                        ToolName = orderData.ToolName,
                        IncomingQuantity = orderData.Quantity
                    };

                    // Add the new OrderedTool to the context
                    _context.OrderedTools.Add(newOrderedTool);
                }

                // Save changes to the database
                _context.SaveChanges();

                return Ok(); // Return success status
            }
            catch (Exception ex)
            {
                // Log the error for debugging purposes
                Console.WriteLine($"An error occurred while inserting data: {ex.Message}");

                // Return a more descriptive error message to the client
                return StatusCode(500, "An error occurred while inserting data. Please try again later.");
            }
        }





    }
}
