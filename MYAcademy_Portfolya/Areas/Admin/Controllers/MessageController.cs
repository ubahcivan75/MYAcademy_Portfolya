using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolya.Context;

namespace MYAcademy_Portfolya.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolioContext _context;

        public MessageController(PortfolioContext context)
        {
            _context = context;
        }

        // ================= GELEN KUTUSU =================
        public IActionResult Index(string? search, string? sort, string? status,int? open)
        {
            if (open.HasValue)
            {
                var opened = _context.Messages.Find(open.Value);
                if (opened != null && !opened.IsRead)
                {
                    opened.IsRead = true;
                    _context.SaveChanges();
                }
                ViewBag.OpenId = open.Value;
            }

            var query = _context.Messages.AsQueryable();

            // 1) Arama: gönderen adı, e-posta veya konuya göre
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(x => x.NameSurname.Contains(search)
                                      || x.Email.Contains(search)
                                      || x.Subject.Contains(search));
            }

            // 2) Durum filtresi
            if (status == "unread")
                query = query.Where(x => !x.IsRead);
            else if (status == "read")
                query = query.Where(x => x.IsRead);

            // 3) Tarihe göre sıralama (varsayılan: en yeni en üstte)
            if (sort == "asc")
                query = query.OrderBy(x => x.SendDate);
            else
                query = query.OrderByDescending(x => x.SendDate);

            // Formda seçili değerler kalsın diye geri gönderiyoruz
            ViewBag.Search = search;
            ViewBag.Sort = sort ?? "desc";
            ViewBag.Status = status ?? "all";

            // 4) Sayaçlar (filtreden bağımsız, tüm mesajlar üzerinden)
            ViewBag.UnreadCount = _context.Messages.Count(x => !x.IsRead);
            ViewBag.TotalCount = _context.Messages.Count();

            return View(query.ToList());
        }

        // ================= OKUNDU / OKUNMADI =================
        public IActionResult ToggleRead(int id, string? search, string? sort, string? status)
        {
            var value = _context.Messages.Find(id);
            if (value != null)
            {
                value.IsRead = !value.IsRead;   // sadece durum değişiyor, içerik değil
                _context.SaveChanges();
            }
            return RedirectToAction("Index", new { search, sort, status });
        }

        // ================= SİLME =================
        [HttpPost]
        public IActionResult DeleteMessage(int id, string? search, string? sort, string? status)
        {
            var value = _context.Messages.Find(id);
            if (value != null)
            {
                _context.Messages.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", new { search, sort, status });
        }
    }
}