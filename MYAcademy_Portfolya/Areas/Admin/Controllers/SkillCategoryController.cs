using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolya.Context;
using MYAcademy_Portfolya.Entites;

namespace MYAcademy_Portfolya.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillCategoryController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillCategoryController(PortfolioContext context)
        {
            _context = context;
        }
        public IActionResult SkillCategoryList()
        {

            var values = _context.SkillCategories
                .Include(x => x.Skills)              // Listede yetenek sayısını göstermek için
                .OrderBy(x => x.DisplayOrder)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSkillCategory() 
        {
            return View();
        }        
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSkillCategory(SkillCategory model)
        {
            if (!ModelState.IsValid) return View(model);

            _context.SkillCategories.Add(model);
            _context.SaveChanges();
            TempData["Success"] = "Kategori eklendi.";
            return RedirectToAction(nameof(SkillCategoryList));
        }

        [HttpGet]
        public IActionResult UpdateSkillCategory(int id)
        {
            var value = _context.SkillCategories.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSkillCategory(SkillCategory model)
        {
            if (!ModelState.IsValid) return View(model);

            _context.SkillCategories.Update(model);
            _context.SaveChanges();
            TempData["Success"] = "Kategori güncellendi.";
            return RedirectToAction(nameof(SkillCategoryList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSkillCategory(int id)
        {
            var value = _context.SkillCategories.Find(id);
            if (value != null)
            {
                _context.SkillCategories.Remove(value);   // Yetenekleri de cascade ile silinir
                _context.SaveChanges();
                TempData["Success"] = "Kategori ve içindeki yetenekler silindi.";
            }
            return RedirectToAction(nameof(SkillCategoryList));
        }
    }
}
