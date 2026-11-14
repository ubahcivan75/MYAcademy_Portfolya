using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolya.Context;

namespace MYAcademy_Portfolya.ViewComponents.DefaultViewComponents
{
    public class _DefaultExperienceComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultExperienceComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Experiences
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.StartDate)
                .ToList();
            return View(values); ;
        }
    }
}
