using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_Portfolya.Controllers
{
    public class CvController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public CvController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Download()
        {
            var path = Path.Combine(_env.ContentRootPath, "App_Data", "cv", "cv.pdf");

            if (!System.IO.File.Exists(path))
                return Redirect("/"); // CV yüklenmemişse ana sayfaya dön

            // 3. parametre: ziyaretçinin bilgisayarına inecek dosyanın adı
            return PhysicalFile(path, "application/pdf", "Umit_Bahcivan_CV.pdf");
        }
    }
}