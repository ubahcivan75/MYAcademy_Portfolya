using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolya.Context;
using Microsoft.EntityFrameworkCore;

namespace MYAcademy_Portfolya.ViewComponents.DefaultViewComponents
{
    public class _DefaultSkillComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;
        public _DefaultSkillComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.SkillCategories
                 .Include(x => x.Skills)          // Her kategorinin yeteneklerini de getir
                 .OrderBy(x => x.DisplayOrder)
                 .ToList();
            return View(values);
        }
    
    }
}
