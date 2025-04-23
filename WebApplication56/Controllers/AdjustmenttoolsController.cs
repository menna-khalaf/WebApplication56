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
    public class AdjustmenttoolsController : Controller
    {
        private readonly doctorContext _context;

        public AdjustmenttoolsController(doctorContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var doctorContext = _context.Adjustmenttools.Include(a => a.Adj).Include(a => a.ToolNameNavigation);
 
            // Pass the filtered orders and tool names to the view
           
            return View(await doctorContext.ToListAsync());
        }


        // GET: Adjustmenttools/Details/5
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

        // GET: Adjustmenttools/Create
        public IActionResult Create()
        {
            ViewData["AdjId"] = new SelectList(_context.Adjustments, "AdjId", "Email");
            ViewData["ToolName"] = new SelectList(_context.Tools, "ToolName", "ToolName");
            return View();
        }

        // POST: Adjustmenttools/Create
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

        // GET: Adjustmenttools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Adjustmenttools == null)
            {
                return NotFound();
            }

            var adjustmenttool = await _context.Adjustmenttools.FindAsync(id);
            if (adjustmenttool == null)
            {
                return NotFound();
            }
            ViewData["AdjId"] = new SelectList(_context.Adjustments, "AdjId", "Email", adjustmenttool.AdjId);
            ViewData["ToolName"] = new SelectList(_context.Tools, "ToolName", "ToolName", adjustmenttool.ToolName);
            return View(adjustmenttool);
        }

        // POST: Adjustmenttools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdjId,ToolName,Balance,ActualQuantity,Difference,Because")] Adjustmenttool adjustmenttool)
        {
            if (id != adjustmenttool.AdjId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adjustmenttool);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdjustmenttoolExists(adjustmenttool.AdjId))
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
            ViewData["AdjId"] = new SelectList(_context.Adjustments, "AdjId", "Email", adjustmenttool.AdjId);
            ViewData["ToolName"] = new SelectList(_context.Tools, "ToolName", "ToolName", adjustmenttool.ToolName);
            return View(adjustmenttool);
        }

        // Method to check if an Adjustmenttool exists by id
        private bool AdjustmenttoolExists(int id)
        {
            return _context.Adjustmenttools.Any(e => e.AdjId == id);
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

        // POST: Adjustmenttools1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Adjustmenttools == null)
            {
                return Problem("Entity set 'doctorContext.Adjustmenttools' is null.");
            }
            var adjustmenttool = await _context.Adjustmenttools.FindAsync(id);
            if (adjustmenttool != null)
            {
                _context.Adjustmenttools.Remove(adjustmenttool);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult UpdateToolNameQuantityAndDifference(int adjId, string newToolName, int actualQuantity, int difference)
        {
            // Retrieve the adjustment tool by its ID
            var adjustmentTool = _context.Adjustmenttools.FirstOrDefault(t => t.AdjId == adjId);

            if (adjustmentTool != null)
            {
                // Update the tool name
                adjustmentTool.ToolName = newToolName;

                // Insert the actual quantity and difference
                var newEntry = new Adjustmenttool
                {
                    ToolName = newToolName,
                    ActualQuantity = actualQuantity,
                    Difference = difference
                };

                _context.Adjustmenttools.Add(newEntry);
                _context.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Adjustment tool not found" });
        }





        private List<string> GetAllToolNames()
        {
            return _context.Tools.Select(t => t.ToolName).ToList();
        }


    }
}
