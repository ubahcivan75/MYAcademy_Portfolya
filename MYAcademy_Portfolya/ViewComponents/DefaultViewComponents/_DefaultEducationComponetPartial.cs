using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolya.Context;

namespace MYAcademy_Portfolya.ViewComponents.DefaultViewComponents
{
    public class _DefaultEducationComponetPartial : ViewComponent 
    {
        private readonly PortfolioContext _context;

        public _DefaultEducationComponetPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Educations
                 .Where(x => x.IsActive)                 // pasifler sitede görünmez
                 .OrderByDescending(x => x.StartDate)    // en yeni üstte
                 .ToList();
            return View(values);
        }

    }
}
