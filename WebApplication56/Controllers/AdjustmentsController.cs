using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication56.Models;

namespace WebApplication56.Controllers
{
    public class AdjustmentsController : Controller
    {
        private readonly doctorContext _context;

        public AdjustmentsController(doctorContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? adjId)
        {
            // Retrieve the AdjId from session if not provided
            if (!adjId.HasValue)
            {
                adjId = HttpContext.Session.GetInt32("AdjId");
            }

            // Pass adjId to the ViewBag
            ViewBag.AdjId = adjId;

            // Retrieve Adjustmenttool entity and related Adjustment information
            var adjustmentTool = await _context.Adjustmenttools
                .Include(at => at.Adj)
                .Include(at => at.ToolNameNavigation)
                .FirstOrDefaultAsync();


            // Retrieve tool names from the database
            var toolNames = await _context.Tools
                .Select(t => t.ToolName)
                .ToListAsync();

            // Pass the filtered orders and tool names to the view
            ViewData["ToolNames"] = toolNames;
            // If adjustmentTool is null, create a new instance
            if (adjustmentTool == null)
            {
                adjustmentTool = new Adjustmenttool(); // You may need to initialize properties accordingly
            }

            return View(adjustmentTool);
        }






        private List<string> GetAllToolNames()
        {
            return _context.Tools.Select(t => t.ToolName).ToList();
        }



        [HttpGet]
        public IActionResult GetBalanceForTool(string toolName)
        {
            // Retrieve the balance for the selected tool name from the database
            var balance = _context.Tools.FirstOrDefault(t => t.ToolName == toolName)?.Balance;
            return Content(balance.ToString());
        }










        // GET: Adjustments/Details 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Adjustments == null)
            {
                return NotFound();
            }

            var adjustment = await _context.Adjustments
                .Include(a => a.EmailNavigation)
                .FirstOrDefaultAsync(m => m.AdjId == id);
            if (adjustment == null)
            {
                return NotFound();
            }

            return View(adjustment);
        }

        // GET: Adjustments/Create
        public IActionResult Create()
        {

            string userEmail = HttpContext.Session.GetString("UserEmail");

            // Pass the email to the view
            ViewBag.UserEmail = userEmail;

            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email");
            return View();
        }

        // POST: Adjustments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdjId,AdjDate,Email,Description")] Adjustment adjustment)
        {
            adjustment.Email = HttpContext.Session.GetString("UserEmail");
            if (ModelState.IsValid)
            {
                _context.Add(adjustment);
                await _context.SaveChangesAsync();


                // Save the AdjId in session
                HttpContext.Session.SetInt32("AdjId", adjustment.AdjId);

                // Redirect to Index action
                return RedirectToAction(nameof(Index));
            }
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email", adjustment.Email);
            return View(adjustment);
        }



        // GET: Adjustments/Edit 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Adjustments == null)
            {
                return NotFound();
            }

            var adjustment = await _context.Adjustments.FindAsync(id);
            if (adjustment == null)
            {
                return NotFound();
            }
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email", adjustment.Email);
            return View(adjustment);
        }

        // POST: Adjustments/Edit 
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdjId,AdjDate,Email,Description")] Adjustment adjustment)
        {
            if (id != adjustment.AdjId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adjustment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdjustmentExists(adjustment.AdjId))
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
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email", adjustment.Email);
            return View(adjustment);
        }

        // GET: Adjustments/Delete 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Adjustments == null)
            {
                return NotFound();
            }

            var adjustment = await _context.Adjustments
                .Include(a => a.EmailNavigation)
                .FirstOrDefaultAsync(m => m.AdjId == id);
            if (adjustment == null)
            {
                return NotFound();
            }

            return View(adjustment);
        }

        // POST: Adjustments/Delete 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Adjustments == null)
            {
                return Problem("Entity set 'doctorContext.Adjustments'  is null.");
            }
            var adjustment = await _context.Adjustments.FindAsync(id);
            if (adjustment != null)
            {
                _context.Adjustments.Remove(adjustment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdjustmentExists(int id)
        {
          return (_context.Adjustments?.Any(e => e.AdjId == id)).GetValueOrDefault();
        }




        public class AdjustmentData
        {
            public int AdjId { get; set; } // ID retrieved from session
            public string ToolName { get; set; }
            public int Balance { get; set; }
            public int ActualQuantity { get; set; }
            public int Difference { get; set; }
        }

        [HttpPost]
        public IActionResult InsertAllAdjustments([FromBody] List<AdjustmentData> adjustmentDataList)
        {
            try
            {
                if (adjustmentDataList == null || !adjustmentDataList.Any())
                {
                    return BadRequest("No data received");
                }

                foreach (var adjustmentData in adjustmentDataList)
                {
                    // Create a new Adjustmenttool object and set its properties
                    var adjustmentTool = new Adjustmenttool
                    {
                        AdjId = adjustmentData.AdjId,
                        ToolName = adjustmentData.ToolName,
                        Balance = adjustmentData.Balance,
                        ActualQuantity = adjustmentData.ActualQuantity,
                        Difference = adjustmentData.Difference
                    };

                    // Add the new Adjustmenttool to the context
                    _context.Adjustmenttools.Add(adjustmentTool);
                }

                // Save changes to the database
                _context.SaveChanges();

                return Ok(new { success = true }); // Return success status
            }
            catch (Exception ex)
            {
                // Log the error for debugging purposes
                Console.WriteLine($"An error occurred while inserting adjustment data: {ex.Message}");

                // Return a more descriptive error message to the client
                return StatusCode(500, "An error occurred while inserting adjustment data. Please try again later.");
            }
        }

    }
}
