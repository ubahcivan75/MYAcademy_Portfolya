using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolya.Models;
using System.Diagnostics;

namespace MYAcademy_Portfolya.Controllers
{
    // Sadece hata sayfası için tutuluyor (Program.cs → UseExceptionHandler("/Home/Error"))
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}