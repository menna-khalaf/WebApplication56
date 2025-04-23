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
    public class Repurshase : Controller
    {


        private readonly doctorContext _context;

        public Repurshase(doctorContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var tools = _context.Tools
          .Where(t => t.Storequantity * 0.2 >= t.Balance)
          .ToList();
            return View(tools);

        }
    }
}
