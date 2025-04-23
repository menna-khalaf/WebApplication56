using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WebApplication56.Models;
using static WebApplication56.Controllers.OrdersController;

namespace WebApplication56.Controllers
{
    public class ToolsController : Controller
    {
        private readonly doctorContext _context;

        public ToolsController(doctorContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var generatedOrdId = HttpContext.Session.GetString("GeneratedOrdId");

            var tools = _context.Tools.ToList();

            // Pass OrdId to the view using ViewBag
            ViewBag.OrdId = generatedOrdId;
            return View(tools);
        }
        [HttpPost]
        public IActionResult Insertion([FromBody] List<OrderData> ordersData)
        {
            try
            {
                if (ordersData == null || !ordersData.Any())
                {
                    return BadRequest("No data received");
                }

                foreach (var orderData in ordersData)
                {
                    // Assuming you have an Entity Framework DbContext named "_context"
                    var newOrderedTool = new OrderedTool
                    {
                        OrdId = HttpContext.Session.GetString("GeneratedOrdId"), // Use the ordId retrieved from the session
                        ToolName = orderData.ToolName,
                        OutgoingQuantity = orderData.Quantity
                    };

                    _context.OrderedTools.Add(newOrderedTool);
                }

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
