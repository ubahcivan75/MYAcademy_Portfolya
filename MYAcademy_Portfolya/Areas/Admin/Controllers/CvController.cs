using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_Portfolya.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CvController : Controller
    {
        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB
        private readonly IWebHostEnvironment _env;

        public CvController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // ContentRootPath = projenin ana klasörü (wwwroot'un bir üstü)
        private string CvFolder => Path.Combine(_env.ContentRootPath, "App_Data", "cv");
        private string CvPath => Path.Combine(CvFolder, "cv.pdf");

        // ================= SAYFA =================
        [HttpGet]
        public IActionResult Index()
        {
            LoadCvInfo();
            return View();
        }

        // ================= YÜKLE / GÜNCELLE =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IFormFile? cvFile)
        {
            var error = ValidatePdf(cvFile);
            if (error != null)
            {
                ViewBag.Error = error;
                LoadCvInfo();
                return View();
            }

            Directory.CreateDirectory(CvFolder);

            // 1) Önce geçici dosyaya yaz
            var tempPath = Path.Combine(CvFolder, "cv_upload.tmp");
            using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                cvFile!.CopyTo(stream);
            }

            // 2) Yazma başarılıysa eski CV'nin yerine koy (true = üzerine yaz)
            System.IO.File.Move(tempPath, CvPath, true);

            ViewBag.Success = "CV başarıyla güncellendi.";
            LoadCvInfo();
            return View();
        }

        // ================= ÖNİZLE (tarayıcıda açar) =================
        public IActionResult Preview()
        {
            if (!System.IO.File.Exists(CvPath))
                return RedirectToAction("Index");

            return PhysicalFile(CvPath, "application/pdf"); // dosya adı yok → tarayıcıda açılır
        }

        // ================= SİL =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete()
        {
            if (System.IO.File.Exists(CvPath))
                System.IO.File.Delete(CvPath);

            return RedirectToAction("Index");
        }

        // ================= YARDIMCI METOTLAR =================
        private void LoadCvInfo()
        {
            var info = new FileInfo(CvPath);
            ViewBag.CvExists = info.Exists;

            if (info.Exists)
            {
                ViewBag.CvSize = (info.Length / 1024.0).ToString("0") + " KB";
                ViewBag.CvDate = info.LastWriteTime.ToString("dd.MM.yyyy HH:mm");
            }
        }

        private string? ValidatePdf(IFormFile? file)
        {
            // 1) Dosya seçilmiş mi?
            if (file == null || file.Length == 0)
                return "Lütfen bir PDF dosyası seçin.";

            // 2) Boyut kontrolü
            if (file.Length > MaxFileSize)
                return "Dosya boyutu en fazla 5 MB olabilir.";

            // 3) Uzantı kontrolü
            if (Path.GetExtension(file.FileName).ToLower() != ".pdf")
                return "Sadece .pdf uzantılı dosyalar yüklenebilir.";

            // 4) İçerik kontrolü: her gerçek PDF "%PDF-" ile başlar
            using (var stream = file.OpenReadStream())
            {
                var header = new byte[5];
                int read = stream.Read(header, 0, 5);

                if (read < 5 || System.Text.Encoding.ASCII.GetString(header) != "%PDF-")
                    return "Seçilen dosya geçerli bir PDF değil.";
            }

            return null; // hata yok
        }
    }
}