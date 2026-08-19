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
        public DbSet<Asistencia> Asistencias { get; set; }

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

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Curso)
                .WithMany()
                .HasForeignKey(a => a.CursoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Estudiante)
                .WithMany()
                .HasForeignKey(a => a.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Un estudiante solo puede tener un registro de asistencia por curso y fecha.
            modelBuilder.Entity<Asistencia>()
                .HasIndex(a => new { a.CursoId, a.EstudianteId, a.Fecha })
                .IsUnique();


            modelBuilder.Entity<Carrera>().HasData(
                new Carrera { Id = 1, Nombre = "Ingeniería en Sistemas", Codigo = "ING-SIS" },
                new Carrera { Id = 2, Nombre = "Administración de Empresas", Codigo = "ADM-EMP" }
            );

            // Docente y Estudiante de demostración: el correo es la clave que los une
            // con el usuario de Identity creado en Program.cs (ver GetOrCreateEstudianteAsync
            // y PanelDocenteController, que buscan por correo). Así, las credenciales
            // publicadas en la documentación funcionan de inmediato sin pasos manuales.
            modelBuilder.Entity<Docente>().HasData(
                new Docente { Id = 1, Nombre = "Carlos Brenes Solano", Correo = "docente1@ura.com", Especialidad = "Bases de Datos" }
            );

            modelBuilder.Entity<Estudiante>().HasData(
                new Estudiante { Id = 1, Nombre = "Nicole Vargas Solano", Correo = "estudiante1@ura.com", CarreraId = 1 }
            );

            modelBuilder.Entity<Curso>().HasData(
                new Curso { Id = 1, Codigo = "SC-301", Nombre = "Estructuras de Datos", Creditos = 4, CarreraId = 1, DocenteId = 1, Modalidad = "Presencial", Sede = "San José", Horario = "Lun/Mié 18:00-20:00", Estado = "Activo" },
                new Curso { Id = 2, Codigo = "SC-220", Nombre = "Bases de Datos II", Creditos = 3, CarreraId = 1, Modalidad = "Virtual", Sede = "Online", Horario = "Mar/Jue 19:00-21:00", Estado = "Activo" },
                new Curso { Id = 3, Codigo = "ADM-101", Nombre = "Introducción a la Administración", Creditos = 3, CarreraId = 2, Modalidad = "Presencial", Sede = "San José", Horario = "Vie 08:00-11:00", Estado = "Activo" }
            );

            modelBuilder.Entity<Matricula>().HasData(
                new Matricula { Id = 1, EstudianteId = 1, CursoId = 1, Fecha = new DateTime(2026, 7, 1), Estado = "Activa" }
            );
        }

    }
}
