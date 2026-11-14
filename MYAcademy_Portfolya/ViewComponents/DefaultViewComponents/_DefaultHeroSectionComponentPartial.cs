using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolya.Context;

namespace MYAcademy_Portfolya.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroSectionComponentPartial : ViewComponent
    {

        private readonly PortfolioContext _context;

        public _DefaultHeroSectionComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.HeroSections.FirstOrDefault();
            ViewBag.v1 = values?.Title;
            ViewBag.v2 = values?.SubTitle;
            ViewBag.v3 = values?.Description;
            ViewBag.v4 = values?.ImageUrl;

            // Sosyal bağlantılar: Admin → İletişim Bilgileri sayfasından gelir
            var contact = _context.Contacts.FirstOrDefault();
            ViewBag.Github = contact?.Github;
            ViewBag.Linkedin = contact?.Linkein;
            ViewBag.Mail = contact?.Emil;

            return View();
        }
    }
}
