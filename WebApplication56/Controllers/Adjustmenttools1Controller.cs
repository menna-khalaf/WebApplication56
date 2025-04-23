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
    public class Adjustmenttools1Controller : Controller
    {
        private readonly doctorContext _context;

        public Adjustmenttools1Controller(doctorContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var doctorContext = _context.Adjustmenttools
                .Include(a => a.Adj)
                .Include(a => a.ToolNameNavigation)
                .Where(a => !a.Active) // Exclude rows where Active is true
                .Select(a => new Adjustmenttool // Create a new object with selected properties
                {
                    AdjId = a.AdjId,
                    Balance = a.Balance,
                    ActualQuantity = a.ActualQuantity,
                    Difference = a.Difference,
                    Because = a.Because,
                    Active = a.Active,
                    Adj = new Adjustment // Include AdjDate from Adjustment model
                    {
                        AdjId = a.Adj.AdjId,
                        AdjDate = a.Adj.AdjDate,
                        Email = a.Adj.Email,
                        Description = a.Adj.Description
                    },
                    ToolNameNavigation = a.ToolNameNavigation
                });

            return View(await doctorContext.ToListAsync());
        }

        public class ActivateRequest
        {
            public int AdjId { get; set; }
            public string ToolName { get; set; }
        }

        [HttpPost]
        public IActionResult ActivateRow([FromBody] ActivateRequest request)
        {
            try
            {
                var adjustment = _context.Adjustmenttools.FirstOrDefault(a => a.AdjId == request.AdjId && a.ToolName == request.ToolName);
                if (adjustment != null)
                {
                    adjustment.Active = true; // Or any other logic to update the row
                    _context.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Row not found" });
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return Json(new { success = false, message = ex.Message });
            }
        }











        // GET: Adjustmenttools1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Adjustmenttools == null)
            {
                return NotFound();
            }

            var adjustmenttool = await _context.Adjustmenttools
                .Include(a => a.Adj)
                .Include(a => a.ToolNameNavigation)
                .FirstOrDefaultAsync(m => m.AdjId == id);
            if (adjustmenttool == null)
            {
                return NotFound();
            }

            return View(adjustmenttool);
        }


        // GET: Adjustmenttools1/Create
        public IActionResult Create()
        {
            ViewData["AdjId"] = new SelectList(_context.Adjustments, "AdjId", "Email");
            ViewData["ToolName"] = new SelectList(_context.Tools, "ToolName", "ToolName");
            return View();
        }

        // POST: Adjustmenttools1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdjId,ToolName,Balance,ActualQuantity,Difference,Because")] Adjustmenttool adjustmenttool)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adjustmenttool);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdjId"] = new SelectList(_context.Adjustments, "AdjId", "Email", adjustmenttool.AdjId);
            ViewData["ToolName"] = new SelectList(_context.Tools, "ToolName", "ToolName", adjustmenttool.ToolName);
            return View(adjustmenttool);
        }

        // GET: Adjustmenttools1/Edit/5
        public async Task<IActionResult> Edit(int? adjId, string toolName)
        {
            if (adjId == null || string.IsNullOrEmpty(toolName))
            {
                return NotFound();
            }

            var adjustmenttool = await _context.Adjustmenttools.FindAsync(adjId, toolName);
            if (adjustmenttool == null)
            {
                return NotFound();
            }

            ViewData["AdjId"] = new SelectList(_context.Adjustments, "AdjId", "Email", adjustmenttool.AdjId);
            ViewData["ToolName"] = new SelectList(_context.Tools, "ToolName", "ToolName", adjustmenttool.ToolName);

            return View(adjustmenttool);
        }

        // POST: Adjustmenttools1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int adjId, [Bind("AdjId,ToolName,Balance,ActualQuantity,Difference,Because")] Adjustmenttool adjustmenttool)
        {
            if (adjId != adjustmenttool.AdjId)
            {
                return Json(new { success = false, message = "Invalid Adjustment ID" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adjustmenttool);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdjustmenttoolExists(adjustmenttool.AdjId))
                    {
                        return Json(new { success = false, message = "Adjustment tool does not exist" });
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, errors });
        }



        // GET: Adjustmenttools1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Adjustmenttools == null)
            {
                return NotFound();
            }

            var adjustmenttool = await _context.Adjustmenttools
                .Include(a => a.Adj)
                .Include(a => a.ToolNameNavigation)
                .FirstOrDefaultAsync(m => m.AdjId == id);
            if (adjustmenttool == null)
            {
                return NotFound();
            }

            return View(adjustmenttool);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int adjId, string toolName)
        {
            try
            {
                if (_context.Adjustmenttools == null)
                {
                    return Problem("Entity set 'doctorContext.Adjustmenttools' is null.");
                }

                var adjustmenttool = await _context.Adjustmenttools.FindAsync(adjId, toolName);
                if (adjustmenttool != null)
                {
                    _context.Adjustmenttools.Remove(adjustmenttool);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Deleted successfully" });
                }
                return NotFound(new { message = "Item not found" });
            }
            catch (Exception ex)
            {
                // Log the error (use logging framework or simple log)
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred during deletion", error = ex.Message });
            }
        }




        private bool AdjustmenttoolExists(int id)
        {
            return (_context.Adjustmenttools?.Any(e => e.AdjId == id)).GetValueOrDefault();
        }

    }
}
