using Curso.Context;
using Curso.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso.Controllers
{
    public class LoginController : Controller
    {
        string cadenaConexion = "DefaultConnection";
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LoginController> _logger;
        private readonly IConfiguration _configuration;

        public LoginController(ILogger<LoginController> logger, ApplicationDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            cadenaConexion = configuration.GetConnectionString(cadenaConexion);
        }

        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string usuario, string contraseña)
        {
            // Busca el usuario en la base de datos
            var user = _context.LoginUser.FirstOrDefault(u => u.Usuario == usuario);

            if (user != null && user.Password == contraseña) // Validación básica
            {
                // Usuario válido, redirigir al dashboard
                //Session["Usuario"] = usuario;
                HttpContext.Session.SetString("Usuario", usuario);

                //var User = HttpContext.Session.GetString("Usuario");

                return RedirectToAction("Dashboard", "Inicio");
            }
            else
            {
                // Usuario inválido
                ViewBag.Error = "Credenciales incorrectas.";
                return View("Index");
            }
        }
    }
}
