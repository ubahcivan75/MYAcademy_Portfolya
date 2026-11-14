using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolya.Entites;
using MYAcademy_Portfolya.Context;

namespace MYAcademy_Portfolya.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly PortfolioContext _context;

        public AboutController(PortfolioContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var values = _context.Abouts.FirstOrDefault() ?? new Entites.About();
            return View(values);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(About model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.AboutId == 0)
                _context.Abouts.Add(model);
            else
                _context.Abouts.Update(model);

            _context.SaveChanges();
            TempData["Success"] = "Bilgileriniz başarıyla kaydedildi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
        
    

