using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProyectoPROGEND.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // public DbSet<EntityName> EntityNames { get; set; }
        public DbSet<PlanEntranamiento> PlanEntranamiento { get; set; }
        public DbSet<Progreso> Progresos { get; set; }
        public DbSet<DatosUser> DatosUsers { get; set; }
        public DbSet<Recetas> Recetas { get; set; }
        public DbSet<UserRecetas> UserRecetas { get; set; }
        public DbSet<UserPlanesEntrenamiento> UserPlanesEntrenamientos { get; set; }

        public DbSet<DatosVistaInicio> DatosVistaInicio { get; set; }
        public DbSet<RelacionEntrenamientoRecetas> RelacionEntrenamientoRecetas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DatosUser>()
                .HasOne(d => d.User)
                .WithMany(u => u.DatosUsuarios)
                .HasForeignKey(d => d.UserId);

            builder.Entity<UserRecetas>()
                .HasOne(urf => urf.User)
                .WithMany(u => u.RecetasUsuarios)
                .HasForeignKey(urf => urf.UserId);

            builder.Entity<UserRecetas>()
                .HasOne(urf => urf.Receta)
                .WithMany()
                .HasForeignKey(urf => urf.RecetaId);

            builder.Entity<UserPlanesEntrenamiento>()
                .HasOne(upf => upf.User)
                .WithMany()
                .HasForeignKey(upf => upf.UserId);

            builder.Entity<UserPlanesEntrenamiento>()
                .HasOne(upf => upf.PlanEntrenamiento)
                .WithMany()
                .HasForeignKey(upf => upf.PlanEntrenamientoId);
        }

    }
}