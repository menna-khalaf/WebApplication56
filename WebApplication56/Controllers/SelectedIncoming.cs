using Microsoft.AspNetCore.Mvc;
using WebApplication56.Models;

namespace WebApplication56.Controllers
{
    public class SelectedIncoming : Controller
    {

        private readonly doctorContext _context;

        public SelectedIncoming(doctorContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: A1Incoming
        public IActionResult A1AnnualIncoming()
        {
            return View();
        }

        [HttpPost]
        public IActionResult a1AnnualIncoming()
        {
            var currentDate = DateTime.Today;

            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Year == currentDate.Year) // Filter by incoming quantity and current year
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }


        // GET: A1FirstHalfIncoming (January to June)
        public IActionResult A1FirstHalfIncoming()
        {
            return View();
        }

        // POST: a1FirstHalfIncoming
        [HttpPost]
        public IActionResult a1FirstHalfIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month >= 1 && ot.Ord.OrdDate.Month <= 6) // Filter by incoming quantity and months 1 to 6
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }

        // GET: A1SecondHalfIncoming (July to December)
        public IActionResult A1SecondHalfIncoming()
        {
            return View();
        }

        // POST: a1SecondHalfIncoming
        [HttpPost]
        public IActionResult a1SecondHalfIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month >= 7 && ot.Ord.OrdDate.Month <= 12) // Filter by incoming quantity and months 7 to 12
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }















        // GET: A1FirstQuarterIncoming (January to March)
        public IActionResult A1FirstQuarterIncoming()
        {
            return View();
        }

        // POST: a1FirstQuarterIncoming
        [HttpPost]
        public IActionResult a1FirstQuarterIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month >= 1 && ot.Ord.OrdDate.Month <= 3) // Filter by incoming quantity and months 1 to 3
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }

        // GET: A1SecondQuarterIncoming (April to June)
        public IActionResult A1SecondQuarterIncoming()
        {
            return View();
        }

        // POST: a1SecondQuarterIncoming
        [HttpPost]
        public IActionResult a1SecondQuarterIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month >= 4 && ot.Ord.OrdDate.Month <= 6) // Filter by incoming quantity and months 4 to 6
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }

        // GET: A1ThirdQuarterIncoming (July to September)
        public IActionResult A1ThirdQuarterIncoming()
        {
            return View();
        }

        // POST: a1ThirdQuarterIncoming
        [HttpPost]
        public IActionResult a1ThirdQuarterIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month >= 7 && ot.Ord.OrdDate.Month <= 9) // Filter by incoming quantity and months 7 to 9
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }

        // GET: A1FourthQuarterIncoming (October to December)
        public IActionResult A1FourthQuarterIncoming()
        {
            return View();
        }

        // POST: a1FourthQuarterIncoming
        [HttpPost]
        public IActionResult a1FourthQuarterIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month >= 10 && ot.Ord.OrdDate.Month <= 12) // Filter by incoming quantity and months 10 to 12
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }






        public IActionResult A1JanuaryIncoming()
        {
            return View();
        }

        // POST: a1JanuaryIncoming
        [HttpPost]
        public IActionResult a1JanuaryIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month == 1) // Filter by incoming quantity and January
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }

        // GET: A1FebruaryIncoming
        public IActionResult A1FebruaryIncoming()
        {
            return View();
        }

        // POST: a1FebruaryIncoming
        [HttpPost]
        public IActionResult a1FebruaryIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month == 2) // Filter by incoming quantity and February
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }




        // GET: A1MarchIncoming
        public IActionResult A1MarchIncoming()
        {
            return View();
        }

        // POST: a1MarchIncoming
        [HttpPost]
        public IActionResult a1MarchIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month == 3) // Filter by incoming quantity and March
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }

        // GET: A1AprilIncoming
        public IActionResult A1AprilIncoming()
        {
            return View();
        }

        // POST: a1AprilIncoming
        [HttpPost]
        public IActionResult a1AprilIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month == 4) // Filter by incoming quantity and April
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }

        // GET: A1MayIncoming
        public IActionResult A1MayIncoming()
        {
            return View();
        }

        // POST: a1MayIncoming
        [HttpPost]
        public IActionResult a1MayIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month == 5) // Filter by incoming quantity and May
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }


        // GET: A1JuneIncoming
        public IActionResult A1JuneIncoming()
        {
            return View();
        }

        // POST: a1JuneIncoming
        [HttpPost]
        public IActionResult a1JuneIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month == 6) // Filter by incoming quantity and June
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }

        // GET: A1JulyIncoming
        public IActionResult A1JulyIncoming()
        {
            return View();
        }

        // POST: a1JulyIncoming
        [HttpPost]
        public IActionResult a1JulyIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month == 7) // Filter by incoming quantity and July
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }



        // GET: A1AugustIncoming
        public IActionResult A1AugustIncoming()
        {
            return View();
        }

        // POST: a1AugustIncoming
        [HttpPost]
        public IActionResult a1AugustIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month == 8) // Filter by incoming quantity and August
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }

        // GET: A1SeptemberIncoming
        public IActionResult A1SeptemberIncoming()
        {
            return View();
        }

        // POST: a1SeptemberIncoming
        [HttpPost]
        public IActionResult a1SeptemberIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month == 9) // Filter by incoming quantity and September
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }

        // GET: A1OctoberIncoming
        public IActionResult A1OctoberIncoming()
        {
            return View();
        }

        // POST: a1OctoberIncoming
        [HttpPost]
        public IActionResult a1OctoberIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month == 10) // Filter by incoming quantity and October
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }




        // GET: A1NovemberIncoming
        public IActionResult A1NovemberIncoming()
        {
            return View();
        }

        // POST: a1NovemberIncoming
        [HttpPost]
        public IActionResult a1NovemberIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month == 11) // Filter by incoming quantity and November
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }

        // GET: A1DecemberIncoming
        public IActionResult A1DecemberIncoming()
        {
            return View();
        }

        // POST: a1DecemberIncoming
        [HttpPost]
        public IActionResult a1DecemberIncoming()
        {
            var toolQuantities = _context.OrderedTools
                .Where(ot => ot.IncomingQuantity > 0 && ot.Ord.OrdDate.Month == 12) // Filter by incoming quantity and December
                .GroupBy(ot => ot.ToolName)
                .Select(group => new
                {
                    ToolName = group.Key,
                    TotalIncomingQuantity = group.Sum(ot => ot.IncomingQuantity)
                })
                .ToList();

            return Json(toolQuantities);
        }


    }
}
