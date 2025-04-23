using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApplication56.Models;
using Microsoft.Extensions.Logging;


namespace WebApplication56.Controllers
{
    public class SelectedTools : Controller
    {

         
            private readonly ILogger<SelectedTools> _logger;
            private readonly doctorContext _context;

            public SelectedTools(ILogger<SelectedTools> logger, doctorContext context)
            {
                _logger = logger;
                _context = context;
            }
            public IActionResult Index()
            {
                return View();
            }

        public IActionResult A1()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }


        [HttpPost]
        public IActionResult Annual()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and current year
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, ClinicName = c.ClinicName })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.Order.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName })
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate)
                    .GroupBy(data => new { data.ClinicName, data.OrdDate })
                    .Select(group => new
                    {
                        ClinicName = group.Key.ClinicName,
                        Date = group.Key.OrdDate.ToString("yyyy-MM-dd"),
                        OutgoingQuantity = group.Sum(g => g.OutgoingQuantity)
                    })
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving annual data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }


        public IActionResult A1FIRSTHALF()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }


        [HttpPost]
        public IActionResult a1FIRSTHALF()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and current year for the first half of the year (January to June)
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month >= 1 && data.OrdDate.Month <= 6) // Filter data by outgoing quantity, tool name, current year, and month range 1 to 6
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving semiannual data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }



        public IActionResult A1SECONDHALF()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }


        [HttpPost]
        public IActionResult a1SECONDHALF()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and current year for the second half of the year (July to December)
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month >= 7 && data.OrdDate.Month <= 12) // Filter data by outgoing quantity, tool name, current year, and month range 7 to 12
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving second half year data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }


        public IActionResult A1FIRSTQUARTER()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }


        [HttpPost]
        public IActionResult a1FIRSTQUARTER()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and current year for the first quarter (January to March)
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month >= 1 && data.OrdDate.Month <= 3) // Filter data by outgoing quantity, tool name, current year, and month range 1 to 3
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving first quarter data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }


        public IActionResult A1SECONDQUARTER()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }


        [HttpPost]
        public IActionResult a1SECONDQUARTER()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and current year for the second quarter (April to June)
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month >= 4 && data.OrdDate.Month <= 6) // Filter data by outgoing quantity, tool name, current year, and month range 4 to 6
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving second quarter data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }



        public IActionResult A1THIRDQUARTER()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }


        [HttpPost]
        public IActionResult a1THIRDQUARTER()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and current year for the third quarter (July to September)
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month >= 7 && data.OrdDate.Month <= 9) // Filter data by outgoing quantity, tool name, current year, and month range 7 to 9
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving third quarter data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }




        public IActionResult A1FOURTHQUARTER()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }

        [HttpPost]
        public IActionResult a1FOURTHQUARTER()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and current year for the fourth quarter (October to December)
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month >= 10 && data.OrdDate.Month <= 12) // Filter data by outgoing quantity, tool name, current year, and month range 10 to 12
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving fourth quarter data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }



        public IActionResult A1MonthJANUARY()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }


        [HttpPost]
        public IActionResult a1MonthJANUARY()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and current year for January
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month == 1) // Filter data by outgoing quantity, tool name, current year, and month (January)
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving January data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }



        public IActionResult A1MONTHFEBRUARY()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }


        [HttpPost]
        public IActionResult a1MONTHFEBRUARY()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and February
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month == 2) // Filter data by outgoing quantity, tool name, current year, and month (February)
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving February data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }



        public IActionResult A1MONTHMARCH()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }


        [HttpPost]
        public IActionResult a1MONTHMARCH()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and March
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month == 3) // Filter data by outgoing quantity, tool name, current year, and month (March)
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving March data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }



        public IActionResult A1MONTHAPRIL()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }


        [HttpPost]
        public IActionResult a1MONTHAPRIL()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and April
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month == 4) // Filter data by outgoing quantity, tool name, current year, and month (April)
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving April data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }




        public IActionResult A1MONTHMAY()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }




        [HttpPost]
        public IActionResult a1MONTHMAY()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and May
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month == 5) // Filter data by outgoing quantity, tool name, current year, and month (May)
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving May data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }



        public IActionResult A1MONTHJUNE()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }



        [HttpPost]
        public IActionResult a1MONTHJUNE()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and June
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month == 6) // Filter data by outgoing quantity, tool name, current year, and month (June)
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving June data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }





        public IActionResult A1MONTHJULY()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }



        [HttpPost]
        public IActionResult a1MONTHJULY()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and July
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month == 7) // Filter data by outgoing quantity, tool name, current year, and month (July)
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving July data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }



        public IActionResult A1MONTHAUGUST()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }


        [HttpPost]
        public IActionResult a1MONTHAUGUST()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and August
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month == 8) // Filter data by outgoing quantity, tool name, current year, and month (August)
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving August data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }





        public IActionResult A1MONTHSEPTEMBER()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }



        [HttpPost]
        public IActionResult a1MONTHSEPTEMBER()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and September
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month == 9) // Filter data by outgoing quantity, tool name, current year, and month (September)
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving September data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }


        public IActionResult A1MONTHOCTOBER()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }




        [HttpPost]
        public IActionResult a1MONTHOCTOBER()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and October
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month == 10) // Filter data by outgoing quantity, tool name, current year, and month (October)
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving October data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }




        public IActionResult A1MONTHNOVEMBER()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }



        [HttpPost]
        public IActionResult a1MONTHNOVEMBER()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and November
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month == 11) // Filter data by outgoing quantity, tool name, current year, and month (November)
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving November data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }


        public IActionResult A1MONTHDECEMBER()
        {
            // Retrieve tool name from session
            string selectedTool = HttpContext.Session.GetString("SelectedToolName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedTool))
            {
                selectedTool = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedTool}");

            // Pass the selected tool name to the view
            return View(model: selectedTool);
        }



        [HttpPost]
        public IActionResult a1MONTHDECEMBER()
        {
            try
            {
                // Retrieve tool name from session
                string toolName = HttpContext.Session.GetString("SelectedToolName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(toolName))
                {
                    return BadRequest("Tool name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and December
                var chartData = _context.Orders
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { ClinicName = c.ClinicName, OrdId = o.OrdId, OrdDate = o.OrdDate }) // Include OrdId in the anonymous type
                    .Join(_context.OrderedTools,
                        oc => oc.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { ClinicName = oc.ClinicName, OrdDate = oc.OrdDate.Date, OutgoingQuantity = ot.OutgoingQuantity, ToolName = ot.ToolName }) // Include ToolName and extract only the date part
                    .Where(data => data.OutgoingQuantity > 0 && data.ToolName == toolName && data.OrdDate.Year == currentDate && data.OrdDate.Month == 12) // Filter data by outgoing quantity, tool name, current year, and month (December)
                    .GroupBy(joinedData => new { joinedData.ClinicName, joinedData.OrdDate }) // Group by clinic name and date
                    .Select(group => new { ClinicName = group.Key.ClinicName, Date = group.Key.OrdDate.ToString("yyyy-MM-dd"), OutgoingQuantity = group.Sum(g => g.OutgoingQuantity) }) // Select clinic names, dates, and sum of outgoing quantities for each date
                    .ToList();

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving December data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }


    }
}

