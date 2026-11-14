using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_Portfolya.ViewComponents.DefaultViewComponents
{
    public class _DefaultStyleComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

