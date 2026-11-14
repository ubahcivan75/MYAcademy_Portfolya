using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolya.Context;

namespace MYAcademy_Portfolya.ViewComponents.DefaultViewComponents
{
    public class _DefaultProjectComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultProjectComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Projects
                .Include(x => x.Category)
                .Where(x => x.IsActive)               // sadece yayındaki projeler
                .OrderByDescending(x => x.ProjectId)  // en yeni proje en başta
                .ToList();

            return View(values);
        }
    }
}
