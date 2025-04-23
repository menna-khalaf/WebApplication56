using Microsoft.AspNetCore.Mvc;
using WebApplication56.Models;

namespace WebApplication56.Controllers
{
    public class SelectedOutgoing : Controller
    {



        private readonly doctorContext _context;

        public SelectedOutgoing(doctorContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult A1()
        {
            return View();
        }

        [HttpPost]
        public IActionResult a1Annual()
        {
            var currentDate = DateTime.Today;

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate.Year == currentDate.Year && ot.Status == true) // Filter by outgoing quantity and current year
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }


        public IActionResult A1FirstAnnual()
        {
            return View();
        }

        [HttpPost]
        public IActionResult a1FirstAnnual()
        {
            var startDate = new DateTime(DateTime.Today.Year, 1, 1); // January 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 6, 30);   // June 30th of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (January to June)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }



        public IActionResult A1SecondAnnual()
        {
            return View();
        }

        [HttpPost]
        public IActionResult a1SecondAnnual()
        {
            var startDate = new DateTime(DateTime.Today.Year, 7, 1);   // July 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 12, 31);  // December 31st of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (July to December)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }


        // GET: FirstQuarter (January to March)
        public IActionResult A1FirstQuarter()
        {
            return View();
        }
        [HttpPost]
        public IActionResult a1FirstQuarter()
        {
            var startDate = new DateTime(DateTime.Today.Year, 1, 1);   // January 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 3, 31);  // March 31st of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (January to March)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }



        // GET: SecondQuarter (April to June)
        public IActionResult A1SecondQuarter()
        {
            return View();
        }


        // POST: a1SecondQuarter
        [HttpPost]
        public IActionResult a1SecondQuarter()
        {
            var startDate = new DateTime(DateTime.Today.Year, 4, 1);   // April 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 6, 30);  // June 30th of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (April to June)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }



        // GET: ThirdQuarter (July to September)
        public IActionResult A1ThirdQuarter()
        {
            return View();
        }


        // POST: a1ThirdQuarter
        [HttpPost]
        public IActionResult a1ThirdQuarter()
        {
            var startDate = new DateTime(DateTime.Today.Year, 7, 1);   // July 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 9, 30);  // September 30th of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (July to September)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }





        // GET: FourthQuarter (October to December)
        public IActionResult A1FourthQuarter()
        {
            return View();
        }


        // POST: a1FourthQuarter
        [HttpPost]
        public IActionResult a1FourthQuarter()
        {
            var startDate = new DateTime(DateTime.Today.Year, 10, 1);   // October 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 12, 31);  // December 31st of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (October to December)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }

        // GET: January
        public IActionResult A1January()
        {
            return View();
        }
        [HttpPost]
        public IActionResult a1January()
        {
            var startDate = new DateTime(DateTime.Today.Year, 1, 1);   // January 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 1, DateTime.DaysInMonth(DateTime.Today.Year, 1));  // Last day of January of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (January)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }
        public IActionResult A1February()
        {
            return View();
        }
        // POST: a1February
        [HttpPost]
        public IActionResult a1February()
        {
            var startDate = new DateTime(DateTime.Today.Year, 2, 1);   // February 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 2, DateTime.DaysInMonth(DateTime.Today.Year, 2));  // Last day of February of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (February)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }
        // GET: March
        public IActionResult A1March()
        {
            return View();
        }

        // POST: a1March
        [HttpPost]
        public IActionResult a1March()
        {
            var startDate = new DateTime(DateTime.Today.Year, 3, 1);   // March 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 3, DateTime.DaysInMonth(DateTime.Today.Year, 3));  // Last day of March of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (March)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }
        // GET: April
        public IActionResult A1April()
        {
            return View();
        }

        [HttpPost]
        public IActionResult a1April()
        {
            var startDate = new DateTime(DateTime.Today.Year, 4, 1);   // April 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 4, DateTime.DaysInMonth(DateTime.Today.Year, 4));  // Last day of April of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (April)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }
        // GET: May
        public IActionResult A1May()
        {
            return View();
        }
        // POST: a1May
        [HttpPost]
        public IActionResult a1May()
        {
            var startDate = new DateTime(DateTime.Today.Year, 5, 1);   // May 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 5, DateTime.DaysInMonth(DateTime.Today.Year, 5));  // Last day of May of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (May)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }
        // GET: June
        public IActionResult A1June()
        {
            return View();
        }
        // POST: a1June
        [HttpPost]
        public IActionResult a1June()
        {
            var startDate = new DateTime(DateTime.Today.Year, 6, 1);   // June 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 6, DateTime.DaysInMonth(DateTime.Today.Year, 6));  // Last day of June of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (June)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }
        // GET: July
        public IActionResult A1July()
        {
            return View();
        }


        // POST: a1July
        [HttpPost]
        public IActionResult a1July()
        {
            var startDate = new DateTime(DateTime.Today.Year, 7, 1);   // July 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 7, DateTime.DaysInMonth(DateTime.Today.Year, 7));  // Last day of July of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (July)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }
        
        // GET: August
        public IActionResult A1August()
        {
            return View();
        }


        // POST: a1August
        [HttpPost]
        public IActionResult a1August()
        {
            var startDate = new DateTime(DateTime.Today.Year, 8, 1);   // August 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 8, DateTime.DaysInMonth(DateTime.Today.Year, 8));  // Last day of August of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (August)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }


        // GET: September
        public IActionResult A1September()
        {
            return View();
        }


        // POST: a1September
        [HttpPost]
        public IActionResult a1September()
        {
            var startDate = new DateTime(DateTime.Today.Year, 9, 1);   // September 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 9, DateTime.DaysInMonth(DateTime.Today.Year, 9));  // Last day of September of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (September)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }
        // GET: October
        public IActionResult A1October()
        {
            return View();
        }


        // POST: a1October
        [HttpPost]
        public IActionResult a1October()
        {
            var startDate = new DateTime(DateTime.Today.Year, 10, 1);   // October 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 10, DateTime.DaysInMonth(DateTime.Today.Year, 10));  // Last day of October of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (October)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }

        // GET: November
        public IActionResult A1November()
        {
            return View();
        }

        // POST: a1November
        [HttpPost]
        public IActionResult a1November()
        {
            var startDate = new DateTime(DateTime.Today.Year, 11, 1);   // November 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 11, DateTime.DaysInMonth(DateTime.Today.Year, 11));  // Last day of November of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (November)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }

        // GET: December
        public IActionResult A1December()
        {
            return View();
        }

        // POST: a1December
        [HttpPost]
        public IActionResult a1December()
        {
            var startDate = new DateTime(DateTime.Today.Year, 12, 1);   // December 1st of the current year
            var endDate = new DateTime(DateTime.Today.Year, 12, DateTime.DaysInMonth(DateTime.Today.Year, 12));  // Last day of December of the current year

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.OutgoingQuantity > 0 && ot.Ord.OrdDate >= startDate && ot.Ord.OrdDate <= endDate && ot.Status == true) // Filter by outgoing quantity and date range (December)
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalOutgoingQuantity = group.Sum(ot => ot.OutgoingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }


    }
}
