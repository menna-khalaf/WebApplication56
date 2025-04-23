using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApplication56.Models;

namespace WebApplication56.Controllers
{
    public class SelectedClinic : Controller
    {

        private readonly ILogger<SelectedTools> _logger;
        private readonly doctorContext _context;

        public SelectedClinic(ILogger<SelectedTools> logger, doctorContext context)
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
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedClinic}");

            // Pass the selected tool name to the view
            return View(model: selectedClinic);
        }



        [HttpPost]
        public IActionResult Annual()
        {
            try
            {
                 
                // Retrieve tool name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and current year
                var salesData = _context.Orders
                .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate)
                .Join(_context.Clinics,
                    o => o.ClinicName,
                    c => c.ClinicName,
                    (o, c) => new { Order = o, Clinic = c })
                .Join(_context.OrderedTools,
                    oc => oc.Order.OrdId,
                    ot => ot.OrdId,
                    (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                .Where(data => data.Clinic.ClinicName == clinicName)
                .Select(data => new {
                    OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                    ToolName = data.OrderedTool.ToolName,
                    OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                })
                .ToList();

                return Json(salesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving annual data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }


        //public IActionResult Annual()
        //{
        //    var currentDate = DateTime.Today;
        //    var salesData = _context.Orders
        //        .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate.Year)
        //        .Join(_context.Clinics,
        //            o => o.ClinicName,
        //            c => c.ClinicName,
        //            (o, c) => new { Order = o, Clinic = c })
        //        .Join(_context.OrderedTools,
        //            oc => oc.Order.OrdId,
        //            ot => ot.OrdId,
        //            (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
        //        .Where(data => data.Clinic.ClinicName == "عيادة التشخيص")
        //        .Select(data => new {
        //            OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
        //            ToolName = data.OrderedTool.ToolName,
        //            OutgoingQuantity = data.OrderedTool.OutgoingQuantity
        //        })
        //        .ToList();

        //    return Json(salesData);
        //}

        public IActionResult A1FIRSTHALF()
        {
            // Retrieve tool name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedClinic}");

            // Pass the selected tool name to the view
            return View(model: selectedClinic);
        }

        //public IActionResult A1FIRSTHALF()
        //{
        //    // Retrieve tool name from session
        //    string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

        //    // Ensure selectedTool is not null or empty
        //    if (string.IsNullOrEmpty(selectedClinic))
        //    {
        //        selectedClinic = "DefaultTool"; // Provide a default tool name or handle it appropriately
        //    }

        //    // Log the selected tool for debugging
        //    _logger.LogInformation($"Selected tool: {selectedClinic}");

        //    // Pass the selected tool name to the view
        //    return View(model: selectedClinic);
        //}


        [HttpPost]
        public IActionResult a1SemiAnnualFirstHalf()
        {
            try
            {
                // Retrieve tool name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and the first half of the current year
                var firstHalfSalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month <= 6)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(firstHalfSalesData);
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
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedTool is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected tool: {selectedClinic}");

            // Pass the selected tool name to the view
            return View(model: selectedClinic);
        }

        [HttpPost]
        public IActionResult a1SemiAnnualSecondHalf()
        {
            try
            {
                // Retrieve tool name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure toolName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the tool name and the second half of the current year
                var secondHalfSalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month > 6)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(secondHalfSalesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving semiannual data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }






        public IActionResult A1QUARTERFIRST()
        {
            // Retrieve tool name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1QUARTERFIRSTPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the first quarter of the current year
                var firstQuarterSalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month <= 3)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(firstQuarterSalesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving first quarter data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }



        public IActionResult A1QUARTERSECOND()
        {
            // Retrieve tool name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default tool name or handle it appropriately
            }

            // Log the selected tool for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1QUARTERSECONDPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the second quarter of the current year
                var secondQuarterSalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month > 3 && o.OrdDate.Month <= 6)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(secondQuarterSalesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving second quarter data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }

        public IActionResult A1QUARTERTHIRD()
        {
            // Retrieve clinic name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default clinic name or handle it appropriately
            }

            // Log the selected clinic for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1QUARTERTHIRDPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the third quarter of the current year
                var thirdQuarterSalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month > 6 && o.OrdDate.Month <= 9)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(thirdQuarterSalesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving third quarter data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }



        public IActionResult A1QUARTERFOURTH()
        {
            // Retrieve clinic name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default clinic name or handle it appropriately
            }

            // Log the selected clinic for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1QUARTERFOURTHPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the fourth quarter of the current year
                var fourthQuarterSalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month > 9 && o.OrdDate.Month <= 12)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(fourthQuarterSalesData);
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
            // Retrieve clinic name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default clinic name or handle it appropriately
            }

            // Log the selected clinic for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1MonthJANUARYPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the month of January
                var januarySalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month == 1)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(januarySalesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving January data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }



        public IActionResult A1MonthFEBRUARY()
        {
            // Retrieve clinic name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default clinic name or handle it appropriately
            }

            // Log the selected clinic for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1MonthFEBRUARYPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the month of February
                var februarySalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month == 2)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(februarySalesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving February data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }



        public IActionResult A1MonthMARCH()
        {
            // Retrieve clinic name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default clinic name or handle it appropriately
            }

            // Log the selected clinic for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1MonthMARCHPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the month of March
                var marchSalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month == 3)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(marchSalesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving March data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }

        public IActionResult A1MonthAPRIL()
        {
            // Retrieve clinic name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default clinic name or handle it appropriately
            }

            // Log the selected clinic for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1MonthAPRILPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the month of April
                var aprilSalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month == 4)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(aprilSalesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving April data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }




        public IActionResult A1MonthMAY()
        {
            // Retrieve clinic name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default clinic name or handle it appropriately
            }

            // Log the selected clinic for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1MonthMAYPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the month of May
                var maySalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month == 5)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(maySalesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving May data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }




        public IActionResult A1MonthJUNE()
        {
            // Retrieve clinic name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default clinic name or handle it appropriately
            }

            // Log the selected clinic for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1MonthJUNEPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the month of June
                var juneSalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month == 6)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(juneSalesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving June data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }



        public IActionResult A1MonthJULY()
        {
            // Retrieve clinic name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default clinic name or handle it appropriately
            }

            // Log the selected clinic for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1MonthJULYPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the month of July
                var julySalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month == 7)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(julySalesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving July data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }



        public IActionResult A1MonthAUGUST()
        {
            // Retrieve clinic name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default clinic name or handle it appropriately
            }

            // Log the selected clinic for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1MonthAUGUSTPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the month of August
                var augustSalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month == 8)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(augustSalesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving August data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }




        public IActionResult A1MonthSEPTEMBER()
        {
            // Retrieve clinic name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default clinic name or handle it appropriately
            }

            // Log the selected clinic for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1MonthSEPTEMBERPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the month of September
                var septemberSalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month == 9)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(septemberSalesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving September data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }




        public IActionResult A1MonthOCTOBER()
        {
            // Retrieve clinic name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default clinic name or handle it appropriately
            }

            // Log the selected clinic for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1MonthOCTOBERPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the month of October
                var octoberSalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month == 10)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(octoberSalesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving October data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }




        public IActionResult A1MonthNOVEMBER()
        {
            // Retrieve clinic name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default clinic name or handle it appropriately
            }

            // Log the selected clinic for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1MonthNOVEMBERPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the month of November
                var novemberSalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month == 11)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(novemberSalesData);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving November data.");

                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }



        public IActionResult A1MonthDECEMBER()
        {
            // Retrieve clinic name from session
            string selectedClinic = HttpContext.Session.GetString("SelectedClinicName");

            // Ensure selectedClinic is not null or empty
            if (string.IsNullOrEmpty(selectedClinic))
            {
                selectedClinic = "DefaultTool"; // Provide a default clinic name or handle it appropriately
            }

            // Log the selected clinic for debugging
            _logger.LogInformation($"Selected clinic: {selectedClinic}");

            // Pass the selected clinic name to the view
            return View(model: selectedClinic);
        }


        [HttpPost]
        public IActionResult a1MonthDECEMBERPOST()
        {
            try
            {
                // Retrieve clinic name from session
                string clinicName = HttpContext.Session.GetString("SelectedClinicName");

                // Ensure clinicName is not null or empty
                if (string.IsNullOrEmpty(clinicName))
                {
                    return BadRequest("Clinic name is not available in the session.");
                }

                // Retrieve current year
                var currentDate = DateTime.Today.Year;

                // Fetch data based on the clinic name and the month of December
                var decemberSalesData = _context.Orders
                    .Where(o => o.Type == "توريد" && o.OrdDate.Year == currentDate && o.OrdDate.Month == 12)
                    .Join(_context.Clinics,
                        o => o.ClinicName,
                        c => c.ClinicName,
                        (o, c) => new { Order = o, Clinic = c })
                    .Join(_context.OrderedTools,
                        oc => oc.Order.OrdId,
                        ot => ot.OrdId,
                        (oc, ot) => new { oc.Order, oc.Clinic, OrderedTool = ot })
                    .Where(data => data.Clinic.ClinicName == clinicName)
                    .Select(data => new {
                        OrderDate = data.Order.OrdDate.ToString("MMM dd yyyy"), // Format the date here
                        ToolName = data.OrderedTool.ToolName,
                        OutgoingQuantity = data.OrderedTool.OutgoingQuantity
                    })
                    .ToList();

                return Json(decemberSalesData);
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
