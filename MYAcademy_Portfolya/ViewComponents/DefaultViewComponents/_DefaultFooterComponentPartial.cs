using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolya.Context;

namespace MYAcademy_Portfolya.ViewComponents.DefaultViewComponents
{
    public class _DefaultFooterComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultFooterComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Sosyal bağlantılar: Admin → İletişim Bilgileri
            var contact = _context.Contacts.FirstOrDefault();
            ViewBag.Github = contact?.Github;
            ViewBag.Linkedin = contact?.Linkein;
            ViewBag.Mail = contact?.Emil;

            // Telif satırındaki isim: Admin → Hakkımda
            ViewBag.NameSurname = _context.Abouts
                .Select(x => x.NameSurName)
                .FirstOrDefault();

            return View();
        }
    }
}