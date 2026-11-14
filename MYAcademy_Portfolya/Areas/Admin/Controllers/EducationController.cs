using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolya.Context;
using MYAcademy_Portfolya.Entites;

namespace MYAcademy_Portfolya.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationController : Controller
    {
        private readonly PortfolioContext _context;

        public EducationController(PortfolioContext context)
        {
            _context = context;
        }

        // ----- listeleme -----
        public IActionResult Index()
        {
            var values = _context.Educations
                    .OrderByDescending(x => x.StartDate)
                    .ToList();
            return View(values);
        }

        // ----- ekleme -----
        [HttpGet]
        public IActionResult CreateEducation()
        {

            return View(new Education { IsActive = true });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEducation(Education education)
        {
            CheckDates(education.StartDate, education.EndDate);
            if (!ModelState.IsValid)
                return View(education);   // hatalarla formu geri göster

            _context.Educations.Add(education);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim bilgisi eklendi.";
            return RedirectToAction("Index");
        }

        // ----- güncelleme -----
        [HttpGet]
        public IActionResult UpdateEducation(int id)
        {
            var value = _context.Educations.Find(id);
            if (value == null)
                return RedirectToAction("Index");

            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateEducation(Education education)
        {

            CheckDates(education.StartDate, education.EndDate);
            if (!ModelState.IsValid)
                return View(education);

            _context.Educations.Update(education);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim bilgisi güncellendi.";
            return RedirectToAction("Index");
        }

        // ----- silme -----
        [HttpPost]
        public IActionResult DeleteEducation(int id)
        {
            var value = _context.Educations.Find(id);
            if (value != null)
            {
                _context.Educations.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Eğitim bilgisi silindi.";
            }
            return RedirectToAction("Index");
        }
        // Bitiş tarihi başlangıçtan önce olamaz
        private void CheckDates(DateTime? start, DateTime? end)
        {
            if (start.HasValue && end.HasValue && end < start)
                ModelState.AddModelError("EndDate", "Bitiş tarihi başlangıç tarihinden önce olamaz.");
        }
    }
}