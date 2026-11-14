using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolya.Context;

namespace MYAcademy_Portfolya.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLayoutController : Controller
    {
       private readonly PortfolioContext _context;

        public AdminLayoutController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.MessageByTrue = _context.Messages.Where(x => x.IsRead == true).Count();

            return View();
        }
    }
}
