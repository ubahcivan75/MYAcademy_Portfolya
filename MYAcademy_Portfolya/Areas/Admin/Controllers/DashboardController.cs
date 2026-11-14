using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_Portfolya.Areas.Admin.Models;
using MYAcademy_Portfolya.Context;

namespace MYAcademy_Portfolya.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly PortfolioContext _context;

        public DashboardController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                // ---- Sayılar ----
                ProjectCount = _context.Projects.Count(),
                ActiveProjectCount = _context.Projects.Count(x => x.IsActive),
                SkillCount = _context.Skills.Count(),
                ExperienceCount = _context.Experiences.Count(),
                EducationCount = _context.Educations.Count(),
                MessageCount = _context.Messages.Count(),
                UnreadMessageCount = _context.Messages.Count(x => !x.IsRead),

                // ---- Son 5 mesaj (en yeni en üstte) ----
                LastMessages = _context.Messages
                    .OrderByDescending(x => x.SendDate)
                    .Take(5)
                    .ToList(),

                // ---- Son 3 proje (en son eklenen en üstte) ----
                LastProjects = _context.Projects
                    .Include(x => x.Category)
                    .OrderByDescending(x => x.ProjectId)
                    .Take(3)
                    .ToList(),

                // ---- Kategori başına proje sayısı ----
                CategorySummaries = _context.Projects
                    .GroupBy(x => new { x.CategoryId, x.Category.CategoryName })
                    .Select(g => new CategorySummary
                    {
                        CategoryName = g.Key.CategoryName,
                        ProjectCount = g.Count(),
                        ActiveProjectCount = g.Count(p => p.IsActive)
                    })
                    .OrderByDescending(x => x.ProjectCount)
                    .ToList()
            };

            return View(model);
        }
    }
}