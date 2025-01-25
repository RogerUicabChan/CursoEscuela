using Curso.Context;
using Curso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;

namespace Curso.Controllers
{
    public class UsuariosController : Controller
    {
        string cadenaConexion = "DefaultConnection";
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        conexionBaseDatos cbd = new conexionBaseDatos();

        public UsuariosController(ILogger<HomeController> logger, ApplicationDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            cadenaConexion = configuration.GetConnectionString(cadenaConexion);
        }

        //[HttpGet("getjson")]
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    string json = "";

        //    DataTable dt = new DataTable();

        //    string openConnection = "";
        //    openConnection = cbd.Open(cadenaConexion);

        //    string sql = "SELECT * FROM alumno";
        //    dt = cbd.recuperarTabla(sql);

        //    json = JsonConvert.SerializeObject(dt, Formatting.Indented);

        //    string Nombre = "";
        //    string Apellido_P = "";
        //    string Rol = "";

        //    List<AlumnoModel> alumnos = new List<AlumnoModel>();


        //    foreach (DataRow row in dt.Rows)
        //    {
        //        Nombre = row["Nombre"].ToString();
        //        Apellido_P = row["Apellido_P"].ToString();
        //        Rol = row["Rol"].ToString();


        //        AlumnoModel model = new AlumnoModel();
        //        model.Nombre = Nombre;
        //        model.Apellido_P = Apellido_P;
        //        model.Rol = Rol;
        //        alumnos.Add(model);
        //    }


        //    //return Ok(data);
        //    //return View("Index","Usuarios");
        //    return View("Alumno", alumnos);

        //}

        public IActionResult Index()
        {
            var alumnos = _context.Alumnos
                .Select(a => new AlumnoModel
                {
                    Id = a.Id,
                    Nombre = a.Nombre ?? "Sin Nombre", // Control de nulos
                    Apellido_P = a.Apellido_P ?? "Sin Apellido",
                    Rol = a.Rol ?? "Sin Rol"
                }).ToList();

            return View("Alumno", alumnos);
        }


        // Obtener datos de un alumno
        [HttpGet]
        public IActionResult GetAlumno(int id)
        {

            var getEstudiante = _context.Alumnos.FirstOrDefault(a => a.Id == id);

            if (getEstudiante == null)
            {
                return NotFound();
            }

            return Json(new { id = getEstudiante.Id, nombre = getEstudiante.Nombre, apellido_p = getEstudiante.Apellido_P, rol = getEstudiante.Rol });
        }

        // Guardar los cambios del alumno
        [HttpPost]
        public IActionResult EditAlumno([FromBody] AlumnoModel updatedAlumno)
        {
            var alumnoEdit = _context.Alumnos.FirstOrDefault(a => a.Id == updatedAlumno.Id);

            if (alumnoEdit == null)
            {
                return NotFound();
            }

            alumnoEdit.Nombre = updatedAlumno.Nombre;
            alumnoEdit.Apellido_P = updatedAlumno.Apellido_P;
            alumnoEdit.Rol = updatedAlumno.Rol;

            _context.SaveChanges();

            return Ok();
        }

        //Borrar Alumno
        [HttpPost]
        public IActionResult DeleteAlumno(int id)
        {
            var alumno = _context.Alumnos.FirstOrDefault(a => a.Id == id);

            if (alumno == null)
            {
                return NotFound();
            }

            _context.Alumnos.Remove(alumno);
            _context.SaveChanges();

            return Ok();
        }
    }
}
