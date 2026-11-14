using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolya.Context;

namespace MYAcademy_Portfolya.ViewComponents.DefaultViewComponents
{
    
    public class _DefaultAboutComponentPartial: ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultAboutComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Abouts.FirstOrDefault();
            ViewBag.NameSurName = values?.NameSurName;
            ViewBag.JobTitle=values?.JobTitle;
            ViewBag.Location = values?.Location;
            ViewBag.Description = values?.Description;
            ViewBag.Expert = values?.Expert;
            ViewBag.Mail=values?.Mail;
            ViewBag.OpenToWork = values?.OpenToWork;
            return View();
        }

    }
}
