using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_Portfolya.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutMobilSideBarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
