
using Microsoft.AspNetCore.Mvc;
using WebApplication56.Models; // Assuming TaskClinic.Models contains your clinic model

namespace TaskClinic.Controllers
{
    public class ClinicController : Controller
    {
        private readonly doctorContext _context; // Assuming doctorContext is your database context

        public ClinicController(doctorContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //var clinicNames = _context.clinics.Select(c => c.Clinic_name).ToList();
            var clincs = _context.Clinics.ToList();
            return View(clincs);

            // Pass the list of clinic names to the view
            // return View(clinicNames);
            // Your Index action code here
        }

        // Example action where clinic is selected
        [HttpPost]
        public IActionResult SelectClinic(string ClinicName)
        {
            HttpContext.Session.SetString("SelectedClinic", ClinicName);
            return Ok(); // Return an HTTP response indicating success
        }















        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                _context.Clinics.Add(clinic);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(clinic);
        }

        public IActionResult Edit(string id)
        {
            var clinic = _context.Clinics.Find(id);
            if (clinic == null)
            {
                return NotFound();
            }
            return View(clinic);
        }

        [HttpPost]
        public IActionResult Edit(Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                _context.Update(clinic);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(clinic);
        }

        public IActionResult Delete(string id)
        {
            var clinic = _context.Clinics.Find(id);
            if (clinic == null)
            {
                return NotFound();
            }
            return View(clinic);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            var clinic = _context.Clinics.Find(id);
            if (clinic != null)
            {
                _context.Clinics.Remove(clinic);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }


    }
}