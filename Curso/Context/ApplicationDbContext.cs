using Curso.Models;
using Curso.Models.Logins;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Curso.Context
{
    [Table("alumno")]
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AlumnoModel> Alumnos { get; set; } // Mapeo tabla "Alumno"
        public DbSet<LoginViewModel> LoginUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlumnoModel>().ToTable("alumno"); // Asegura el mapeo correcto a la tabla física
        }
    }
}
