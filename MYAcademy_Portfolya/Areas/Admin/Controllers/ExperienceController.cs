using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolya.Context;
using MYAcademy_Portfolya.Entites;

namespace MYAcademy_Portfolya.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperienceController : Controller
    {
        private readonly PortfolioContext _context;

        public ExperienceController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ExperienceList()
        {
            var values = _context.Experiences
                .OrderByDescending(x => x.StartDate)
                .ToList();
            return View(values);
        }

        // ----- ekleme -----
        [HttpGet]
        public IActionResult CreateExperience()
        {
            return View(new Experience { IsActive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateExperience(Experience experience)
        {
            CheckDates(experience.StartDate, experience.EndDate);
            if (!ModelState.IsValid)
                return View(experience);

            _context.Experiences.Add(experience);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim eklendi.";
            return RedirectToAction("ExperienceList");
        }

        // ----- güncelleme -----
        [HttpGet]
        public IActionResult UpdateExperience(int id)
        {
            var value = _context.Experiences.Find(id);
            if (value == null)
                return RedirectToAction("ExperienceList");

            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateExperience(Experience experience)
        {
            CheckDates(experience.StartDate, experience.EndDate);
            if (!ModelState.IsValid)
                return View(experience);

            _context.Experiences.Update(experience);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim güncellendi.";
            return RedirectToAction("ExperienceList");
        }

        // ----- silme -----
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteExperience(int id)
        {
            var value = _context.Experiences.Find(id);
            if (value != null)
            {
                _context.Experiences.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Deneyim silindi.";
            }
            return RedirectToAction("ExperienceList");
        }
        // Bitiş tarihi başlangıçtan önce olamaz
        private void CheckDates(DateTime? start, DateTime? end)
        {
            if (start.HasValue && end.HasValue && end < start)
                ModelState.AddModelError("EndDate", "Bitiş tarihi başlangıç tarihinden önce olamaz.");
        }
    }
}