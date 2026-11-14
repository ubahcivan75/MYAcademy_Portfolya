using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolya.Context;
using MYAcademy_Portfolya.Entites;

namespace MYAcademy_Portfolya.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;
        private const long MaxImageSize = 2 * 1024 * 1024;   // 2 MB
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        public ProjectController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

        // ================= LİSTELEME =================
        public IActionResult Index()
        {
            var values = _context.Projects.Include(x => x.Category).ToList();
            return View(values);
        }

        // ================= EKLEME =================
        [HttpGet]
        public IActionResult CreateProject()
        {
            LoadCategories();
            return View();
        }

        [HttpPost]
        public IActionResult CreateProject(Project project, IFormFile? imageFile)
        {
            // Formda olmayan alanları validasyondan çıkar
            ModelState.Remove("Category");
            ModelState.Remove("CoverImageUrl");

            // Resim kontrolünü biz yapıyoruz
            if (imageFile == null || imageFile.Length == 0)
            {
                ModelState.AddModelError("CoverImageUrl", "Lütfen bir kapak resmi seçin.");
            }
            else
            {
                var imageError = ValidateImage(imageFile);
                if (imageError != null)
                    ModelState.AddModelError("CoverImageUrl", imageError);
            }

            if (!ModelState.IsValid)
            {
                LoadCategories();          // dropdown boş gelmesin
                return View(project);      // yazılanlar kaybolmasın
            }

            project.CoverImageUrl = SaveImage(imageFile!); // buraya geldiysek resim kesin var
            _context.Projects.Add(project);
            _context.SaveChanges();
            TempData["Success"] = "Proje eklendi.";
            return RedirectToAction("Index");
        }

        // ================= GÜNCELLEME =================
        [HttpGet]
        public IActionResult UpdateProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value == null) return RedirectToAction("Index");

            LoadCategories();
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateProject(Project project, IFormFile? imageFile)
        {
            ModelState.Remove("Category");
            ModelState.Remove("CoverImageUrl");

            // Güncellemede resim zorunlu değil, ama seçildiyse türü doğru olmalı
            if (imageFile != null && imageFile.Length > 0)
            {
                var imageError = ValidateImage(imageFile);
                if (imageError != null)
                    ModelState.AddModelError("CoverImageUrl", imageError);
            }

            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View(project);
            }

            var value = _context.Projects.Find(project.ProjectId);
            if (value == null) return RedirectToAction("Index");

            value.Title = project.Title;
            value.Description = project.Description;
            value.GithubUrl = project.GithubUrl;
            value.CategoryId = project.CategoryId;
            value.IsActive = project.IsActive;

            if (imageFile != null && imageFile.Length > 0)
            {
                DeleteImage(value.CoverImageUrl);          // eskisini sil
                value.CoverImageUrl = SaveImage(imageFile); // yenisini kaydet
            }

            _context.SaveChanges();
            TempData["Success"] = "Proje güncellendi.";
            return RedirectToAction("Index");
        }

        // ================= SİLME =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value != null)
            {
                DeleteImage(value.CoverImageUrl);
                _context.Projects.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Proje silindi.";
            }
            return RedirectToAction("Index");
        }

        // ================= YARDIMCI METOTLAR =================
        private void LoadCategories()
        {
            ViewBag.Categories = _context.Categories
                .Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryId.ToString()
                })
                .ToList();
        }

        // Hata varsa mesajını, yoksa null döner
        private string? ValidateImage(IFormFile file)
        {
            if (file.Length > MaxImageSize)
                return "Görsel en fazla 2 MB olabilir.";

            var ext = Path.GetExtension(file.FileName).ToLower();
            if (!_allowedExtensions.Contains(ext))
                return "Sadece .jpg, .jpeg, .png ve .webp dosyaları yüklenebilir.";

            return null;
        }

        private string SaveImage(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLower();
            var folder = Path.Combine(_env.WebRootPath, "images", "projects");
            Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid() + ext;
            var fullPath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/images/projects/" + fileName;
        }

        private void DeleteImage(string? imageUrl)
        {
            // Sadece bizim yüklediğimiz klasördeki dosyaları sil
            if (string.IsNullOrEmpty(imageUrl) || !imageUrl.StartsWith("/images/projects/"))
                return;

            var fullPath = Path.Combine(_env.WebRootPath,
                imageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);
        }
    }
}