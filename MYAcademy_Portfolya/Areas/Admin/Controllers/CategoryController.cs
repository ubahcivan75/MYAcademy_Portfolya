using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolya.Context;
using MYAcademy_Portfolya.Entites;

namespace MYAcademy_Portfolya.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly PortfolioContext _context;

        public CategoryController(PortfolioContext context)
        {
            _context = context;
        }

        // ================= LİSTELEME =================
        public IActionResult Index()
        {
            var values = _context.Categories
                .Include(x => x.Projects)          // her kategorideki proje sayısı için
                .OrderBy(x => x.CategoryName)
                .ToList();
            return View(values);
        }

        // ================= EKLEME =================
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(Category model)
        {
            CheckDuplicateName(model);

            if (!ModelState.IsValid)
                return View(model);

            _context.Categories.Add(model);
            _context.SaveChanges();
            TempData["Success"] = "Kategori eklendi.";
            return RedirectToAction("Index");
        }

        // ================= GÜNCELLEME =================
        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _context.Categories.Find(id);
            if (value == null)
                return RedirectToAction("Index");

            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(Category model)
        {
            CheckDuplicateName(model);

            if (!ModelState.IsValid)
                return View(model);

            var value = _context.Categories.Find(model.CategoryId);
            if (value == null)
                return RedirectToAction("Index");

            value.CategoryName = model.CategoryName;
            _context.SaveChanges();
            TempData["Success"] = "Kategori güncellendi.";
            return RedirectToAction("Index");
        }

        // ================= SİLME =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int id)
        {
            var value = _context.Categories
                .Include(x => x.Projects)
                .FirstOrDefault(x => x.CategoryId == id);

            if (value == null)
                return RedirectToAction("Index");

            // Koruma: içinde proje varsa silme (yoksa projeler de silinirdi!)
            if (value.Projects.Any())
            {
                TempData["Error"] = $"\"{value.CategoryName}\" kategorisinde {value.Projects.Count} proje var. " +
                                    "Önce bu projeleri başka bir kategoriye taşı ya da sil.";
                return RedirectToAction("Index");
            }

            _context.Categories.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Kategori silindi.";
            return RedirectToAction("Index");
        }

        // ================= YARDIMCI =================
        // Aynı adla ikinci bir kategori oluşmasın
        private void CheckDuplicateName(Category model)
        {
            if (string.IsNullOrWhiteSpace(model.CategoryName))
                return;   // boşsa zaten [Required] yakalar

            model.CategoryName = model.CategoryName.Trim();

            bool exists = _context.Categories.Any(x =>
                x.CategoryName == model.CategoryName &&
                x.CategoryId != model.CategoryId);   // güncellemede kendisiyle çakışmasın

            if (exists)
                ModelState.AddModelError("CategoryName", "Bu adla bir kategori zaten var.");
        }
    }
}