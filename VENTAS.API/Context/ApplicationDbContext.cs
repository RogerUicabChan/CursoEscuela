using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using VENTAS.API.Models;

namespace VENTAS.API.Context
{
    //[Table ("loginuser")]
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> LoginUser { get; set; }
        public DbSet<StudentModel> Alumno { get; set; }
    }
}
