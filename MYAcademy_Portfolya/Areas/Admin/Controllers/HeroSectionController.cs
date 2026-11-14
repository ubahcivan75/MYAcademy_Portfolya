using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolya.Context;
using MYAcademy_Portfolya.Entites;

namespace MYAcademy_Portfolya.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeroSectionController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        public HeroSectionController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var value = _context.HeroSections.FirstOrDefault();

            // Tablo boşsa formu boş bir nesneyle aç
            if (value == null)
                value = new HeroSection();

            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(HeroSection heroSection, IFormFile? imageFile)
        {
            // Resim yolu formdan gelmez, biz üretiriz → otomatik kontrolden çıkar
            ModelState.Remove("ImageUrl");

            var value = _context.HeroSections.FirstOrDefault();
            bool hasNewImage = imageFile != null && imageFile.Length > 0;
            string extension = "";

            // ---- Resim kontrolleri ----
            if (hasNewImage)
            {
                extension = Path.GetExtension(imageFile!.FileName).ToLower();

                if (imageFile.Length > 2 * 1024 * 1024)
                    ModelState.AddModelError("ImageUrl", "Görsel en fazla 2 MB olabilir.");
                else if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".webp")
                    ModelState.AddModelError("ImageUrl", "Sadece .jpg, .jpeg, .png ve .webp dosyaları yüklenebilir.");
            }
            else if (string.IsNullOrEmpty(value?.ImageUrl))
            {
                // İlk kayıt ve resim seçilmemiş
                ModelState.AddModelError("ImageUrl", "Lütfen bir profil resmi seçin.");
            }

            // ---- Hata varsa formu geri göster ----
            if (!ModelState.IsValid)
            {
                heroSection.ImageUrl = value?.ImageUrl ?? "";   // mevcut resmin önizlemesi kaybolmasın
                return View(heroSection);
            }

            // ---- Kaydet ----
            if (value == null)
            {
                value = new HeroSection();
                _context.HeroSections.Add(value);
            }

            value.Title = heroSection.Title;
            value.SubTitle = heroSection.SubTitle;
            value.Description = heroSection.Description;

            if (hasNewImage)
            {
                var folder = Path.Combine(_env.WebRootPath, "images", "hero");
                Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid() + extension;
                using (var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create))
                {
                    imageFile!.CopyTo(stream);
                }

                DeleteImage(value.ImageUrl);                // eski resmi sil
                value.ImageUrl = "/images/hero/" + fileName;
            }

            _context.SaveChanges();
            ViewBag.Success = "Hero Section başarıyla kaydedildi.";
            return View(value);
        }

        private void DeleteImage(string imageUrl)
        {
            // Sadece bizim yüklediğimiz klasördeki dosyaları sil
            if (string.IsNullOrEmpty(imageUrl) || !imageUrl.StartsWith("/images/hero/"))
                return;

            var fullPath = Path.Combine(_env.WebRootPath,
                imageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);
        }
    }
}