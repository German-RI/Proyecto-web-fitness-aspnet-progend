using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProyectoPROGEND.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // public DbSet<EntityName> EntityNames { get; set; }
        public DbSet<PlanEntranamiento> PlanEntranamiento { get; set; }
        public DbSet<Progreso> Progresos { get; set; }
        public DbSet<DatosUser> DatosUsers { get; set; }
        public DbSet<Recetas> Recetas { get; set; }

    }
}