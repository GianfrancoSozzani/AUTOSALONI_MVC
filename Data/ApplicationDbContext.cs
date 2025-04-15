using AUTOSALONI_MVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AUTOSALONI_MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<MARCHE> Marche { get; set; }
        public DbSet<Modello> Modelli { get; set; }
        public DbSet<Automobile> Automobili { get; set; }
    }
}
