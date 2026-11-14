using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_Portfolya.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutStyleComponentPartial: ViewComponent

    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
