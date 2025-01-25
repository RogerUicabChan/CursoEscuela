using Curso.Context;
using Curso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace Curso.Controllers
{
    public class HomeController : Controller
    {
        string cadenaConexion = "DefaultConnection";
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        conexionBaseDatos cbd = new conexionBaseDatos();

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            cadenaConexion = configuration.GetConnectionString(cadenaConexion);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
           DataTable dt = new DataTable();

           string openConnection = "";
            openConnection = cbd.Open(cadenaConexion);

            string sql = "SELECT * FROM alumno";

            dt = cbd.recuperarTabla(sql);

            //var alumnos = _context.Alumnos.ToListAsync();
            //ViewBag.Alumnos = alumnos;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(string nombre, string apellido)
        {
            conexionBaseDatos cbd = new conexionBaseDatos();
            cbd.Open(cadenaConexion);

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
            {
                ModelState.AddModelError("", "Nombre y apellido son obligatorios.");
                return View("Index");
            }

            int folio = 0;
            string sql;

            sql = "INSERT INTO alumno (Nombre, Apellido_P) VALUES ('" + nombre +"','"+ apellido +"')";
            folio = cbd.Insert(sql);


            var alumno = new AlumnoModel
            {
                Nombre = nombre,
                Apellido_P = apellido
            };

            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
