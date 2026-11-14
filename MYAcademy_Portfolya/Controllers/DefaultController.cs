using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_Portfolya.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
