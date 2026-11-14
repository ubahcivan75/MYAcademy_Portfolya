using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolya.Context;
using MYAcademy_Portfolya.Entites;

namespace MYAcademy_Portfolya.Controllers
{
    public class SendMessageController : Controller
    {
        private readonly PortfolioContext _context;

        public SendMessageController(PortfolioContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Message message)
        {
            if (!ModelState.IsValid)
            {
                // Hata mesajlarını topla
                TempData["MessageErrors"] = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                // Kullanıcının yazdıkları kaybolmasın
                TempData["NameSurname"] = message.NameSurname;
                TempData["Email"] = message.Email;
                TempData["Subject"] = message.Subject;
                TempData["MessageDetail"] = message.MessageDetail;

                return RedirectToAction("Index", "Default", null, "contact");
            }

            message.SendDate = DateTime.Now;  // tarihi sunucu belirler
            message.IsRead = false;           // yeni mesaj her zaman okunmamış başlar

            _context.Messages.Add(message);
            _context.SaveChanges();

            TempData["MessageSuccess"] = "Mesajınız başarıyla iletildi! Teşekkür ederiz.";
            return RedirectToAction("Index", "Default", null, "contact");
        }
    }
}