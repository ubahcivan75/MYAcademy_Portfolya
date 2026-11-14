using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolya.Context;
using MYAcademy_Portfolya.Entites;

namespace MYAcademy_Portfolya.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly PortfolioContext _context;

        public ContactController(PortfolioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var value = _context.Contacts.FirstOrDefault();
            if (value == null)
                value = new Contact();

            return View(value);
        }

        [HttpPost]
        public IActionResult Index(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact); // hatalarla formu geri göster
            }

            var value = _context.Contacts.FirstOrDefault();

            if (value == null)
            {
                _context.Contacts.Add(contact);   // ilk kez kaydediliyor
                value = contact;
            }
            else
            {
                value.Description = contact.Description;
                value.Emil = contact.Emil;
                value.Linkein = contact.Linkein;
                value.Location = contact.Location;
                value.Github = contact.Github;
                value.SubDescription = contact.SubDescription;
            }

            _context.SaveChanges();
            ViewBag.Success = "İletişim bilgileri kaydedildi.";
            return View(value);
        }
    }
}