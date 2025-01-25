using Microsoft.AspNetCore.Mvc;
using VENTAS.API.Context;
using System.Text.Json;
using VENTAS.API.Models;

namespace VENTAS.API.Controllers
{
    public class UserController : Controller
    {
        string? cadenaConexion = "DefaultConnection";
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, ApplicationDbContext context, IConfiguration configuration)
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

        [HttpGet("Users")]
        public JsonResult GetUsers()
        {
            var user = _context.LoginUser.ToList();

            return new JsonResult(user);
        }

        //GET: api/Usuarios/1
        [HttpGet("GetById{id:int}")]
        public JsonResult GetUserById(int id)
        {
            var user = _context.LoginUser.Find(id);
            //{
            //    Id = a.Id,
            //    //Usuario = a.Usuario
            //}).ToList();

            return new JsonResult(user);
        }
    }
}
