using Microsoft.AspNetCore.Mvc;

namespace Curso.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            TempData["Usuario"] = HttpContext.Session.GetString("Usuario");
            TempData.Keep("Usuario");
            return View("Dashboard");
        }
    }
}
