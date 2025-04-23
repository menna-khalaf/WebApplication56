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
    public class OrderedToolsController : Controller
    {
        private readonly doctorContext _context;

        public OrderedToolsController(doctorContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var doctorContext = _context.OrderedTools
                                          .Include(o => o.Ord)
                                          .Include(o => o.ToolNameNavigation)
                                          .Where(o => o.Status == false && o.OutgoingQuantity > 0);

            return View(await doctorContext.ToListAsync());
        }
        // GET: OrderedTools/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.OrderedTools == null)
            {
                return NotFound();
            }

            var orderedTool = await _context.OrderedTools
                .Include(o => o.Ord)
                .Include(o => o.ToolNameNavigation)
                .FirstOrDefaultAsync(m => m.OrdId == id);
            if (orderedTool == null)
            {
                return NotFound();
            }

            return View(orderedTool);
        }

        // GET: OrderedTools/Create
        public IActionResult Create()
        {
            ViewData["OrdId"] = new SelectList(_context.Orders, "OrdId", "OrdId");
            ViewData["ToolName"] = new SelectList(_context.Tools, "ToolName", "ToolName");
            return View();
        }

        // POST: OrderedTools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdId,ToolName,IncomingQuantity,OutgoingQuantity,Balance,Status")] OrderedTool orderedTool)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderedTool);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrdId"] = new SelectList(_context.Orders, "OrdId", "OrdId", orderedTool.OrdId);
            ViewData["ToolName"] = new SelectList(_context.Tools, "ToolName", "ToolName", orderedTool.ToolName);
            return View(orderedTool);
        }

        // GET: OrderedTools/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.OrderedTools == null)
            {
                return NotFound();
            }

            var orderedTool = await _context.OrderedTools.FindAsync(id);
            if (orderedTool == null)
            {
                return NotFound();
            }
            ViewData["OrdId"] = new SelectList(_context.Orders, "OrdId", "OrdId", orderedTool.OrdId);
            ViewData["ToolName"] = new SelectList(_context.Tools, "ToolName", "ToolName", orderedTool.ToolName);
            return View(orderedTool);
        }

        // POST: OrderedTools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrdId,ToolName,IncomingQuantity,OutgoingQuantity,Balance,Status")] OrderedTool orderedTool)
        {
            if (id != orderedTool.OrdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderedTool);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderedToolExists(orderedTool.OrdId))
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
            ViewData["OrdId"] = new SelectList(_context.Orders, "OrdId", "OrdId", orderedTool.OrdId);
            ViewData["ToolName"] = new SelectList(_context.Tools, "ToolName", "ToolName", orderedTool.ToolName);
            return View(orderedTool);
        }

        // GET: OrderedTools/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.OrderedTools == null)
            {
                return NotFound();
            }

            var orderedTool = await _context.OrderedTools
                .Include(o => o.Ord)
                .Include(o => o.ToolNameNavigation)
                .FirstOrDefaultAsync(m => m.OrdId == id);
            if (orderedTool == null)
            {
                return NotFound();
            }

            return View(orderedTool);
        }

        // POST: OrderedTools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.OrderedTools == null)
            {
                return Problem("Entity set 'doctorContext.OrderedTools'  is null.");
            }
            var orderedTool = await _context.OrderedTools.FindAsync(id);
            if (orderedTool != null)
            {
                _context.OrderedTools.Remove(orderedTool);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderedToolExists(string id)
        {
          return (_context.OrderedTools?.Any(e => e.OrdId == id)).GetValueOrDefault();
        }


        [HttpPost]
        public IActionResult UpdateStatus(string ordId, string toolName)
        {
            // Update the status of the ordered tool with the specified OrdId and ToolName
            var orderedTool = _context.OrderedTools.FirstOrDefault(o => o.OrdId == ordId && o.ToolName == toolName);
            if (orderedTool != null)
            {
                orderedTool.Status = true;
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }



        [HttpPost]
        public IActionResult UpdateValue(string ordId, string toolName, string newValue)
        {
            var orderedTool = _context.OrderedTools.FirstOrDefault(o => o.OrdId == ordId && o.ToolName == toolName);
            if (orderedTool != null)
            {
                try
                {
                    // Update the quantity
                    orderedTool.OutgoingQuantity = int.Parse(newValue);
                    _context.SaveChanges();

                    // Update the status
                    orderedTool.Status = true;
                    _context.SaveChanges();

                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }
            }
            return Json(new { success = false, error = "Ordered tool not found." });
        }


    }
}
