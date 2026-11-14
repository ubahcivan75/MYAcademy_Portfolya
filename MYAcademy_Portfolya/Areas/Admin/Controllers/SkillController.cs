using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolya.Context;
using MYAcademy_Portfolya.Entites;

namespace MYAcademy_Portfolya.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillController(PortfolioContext context)
        {
            _context = context;
        }

        // Açılır liste için kategorileri ViewBag'e koyan yardımcı metot
        private void LoadCategories(int? selectedId = null)
        {
            ViewBag.Categories = new SelectList(
                _context.SkillCategories.OrderBy(x => x.DisplayOrder).ToList(),
                "SkillCategoryId",   // option value
                "Title",             // option text
                selectedId);
        }

        public IActionResult SkillList()
        {
            var values = _context.Skills
                .Include(x => x.SkillCategory)       // Kategori adını göstermek için
                .OrderBy(x => x.SkillCategory!.DisplayOrder)
                .ThenBy(x => x.SkillId)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSkill()
        {
            LoadCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSkill(Skill model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories(model.SkillCategoryId);   // Hata durumunda liste boş kalmasın
                return View(model);
            }

            _context.Skills.Add(model);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek eklendi.";
            return RedirectToAction(nameof(SkillList));
        }

        [HttpGet]
        public IActionResult UpdateSkill(int id)
        {
            var value = _context.Skills.Find(id);
            if (value == null) return NotFound();

            LoadCategories(value.SkillCategoryId);
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSkill(Skill model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories(model.SkillCategoryId);
                return View(model);
            }

            _context.Skills.Update(model);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek güncellendi.";
            return RedirectToAction(nameof(SkillList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSkill(int id)
        {
            var value = _context.Skills.Find(id);
            if (value != null)
            {
                _context.Skills.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Yetenek silindi.";
            }
            return RedirectToAction(nameof(SkillList));
        }
    }
}