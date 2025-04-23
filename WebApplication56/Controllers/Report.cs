using Microsoft.AspNetCore.Mvc;
using WebApplication56.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebApplication56.Controllers
{
    public class ReportController : Controller
    {
        private readonly doctorContext _context;
        private readonly ILogger<ReportController> _logger;

        public ReportController(doctorContext context, ILogger<ReportController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var toolNames = _context.Tools.Select(t => t.ToolName).ToList();
            var clinicNames = _context.Clinics.Select(c => c.ClinicName).ToList();

            var viewModel = new ToolAndClinicViewModel
            {
                ToolNames = toolNames,
                ClinicNames = clinicNames
            };

            return View(viewModel);
        }

        // Action to handle tool selection
        [HttpPost]
        public IActionResult SelectTool([FromBody] string toolName)
        {
            if (string.IsNullOrEmpty(toolName))
            {
                return Json(new { success = false, message = "Tool name is null or empty." });
            }

            // Save selected tool name in session
            HttpContext.Session.SetString("SelectedToolName", toolName);

            return Json(new { success = true });
        }



        // Action to handle clinic selection
        [HttpPost]
        public IActionResult SelectClinic([FromBody] string clinicName)
        {
            if (string.IsNullOrEmpty(clinicName))
            {
                return Json(new { success = false, message = "clinic name is null or empty." });
            }

            // Save selected clinic name in session
            HttpContext.Session.SetString("SelectedClinicName", clinicName);

            return Json(new { success = true });
        }


    }
}
