using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SistemaMatriculaURA.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Estudiante)
                .WithMany(e => e.Matriculas)
                .HasForeignKey(m => m.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Curso)
                .WithMany(c => c.Matriculas)
                .HasForeignKey(m => m.CursoId)
                .OnDelete(DeleteBehavior.Restrict);

    
            modelBuilder.Entity<Carrera>().HasData(
                new Carrera { Id = 1, Nombre = "Ingeniería en Sistemas", Codigo = "ING-SIS" },
                new Carrera { Id = 2, Nombre = "Administración de Empresas", Codigo = "ADM-EMP" }
            );

            modelBuilder.Entity<Curso>().HasData(
                new Curso { Id = 1, Codigo = "SC-301", Nombre = "Estructuras de Datos", Creditos = 4, CarreraId = 1 },
                new Curso { Id = 2, Codigo = "SC-220", Nombre = "Bases de Datos II", Creditos = 3, CarreraId = 1 },
                new Curso { Id = 3, Codigo = "ADM-101", Nombre = "Introducción a la Administración", Creditos = 3, CarreraId = 2 }
            );
        }

    }
}
