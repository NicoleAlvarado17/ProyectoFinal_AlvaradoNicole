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
        public DbSet<Pago> Pagos { get; set; }

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

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Matricula)
                .WithMany()
                .HasForeignKey(p => p.MatriculaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pago>()
                .HasIndex(p => p.NumeroTransaccion)
                .IsUnique();


            modelBuilder.Entity<Carrera>().HasData(
                new Carrera { Id = 1, Nombre = "Ingeniería en Sistemas", Codigo = "ING-SIS" },
                new Carrera { Id = 2, Nombre = "Administración de Empresas", Codigo = "ADM-EMP" },
                new Carrera { Id = 3, Nombre = "Enfermería", Codigo = "EN" },
                new Carrera { Id = 4, Nombre = "Derecho", Codigo = "DE" },
                new Carrera { Id = 5, Nombre = "Educación", Codigo = "ED" },
                new Carrera { Id = 6, Nombre = "Contabilidad", Codigo = "CO" }
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

            // Plan de estudios (ficticio, con fines demostrativos) de las 6 carreras
            // mostradas en la página de inicio. El costo se calcula a ₡45,000 por
            // crédito, y se usa tanto en el catálogo de cursos como en el
            // comprobante de matrícula (factura) que descarga el estudiante.
            modelBuilder.Entity<Curso>().HasData(
                // Ingeniería en Sistemas (CarreraId = 1)
                new Curso { Id = 1, Codigo = "SC-301", Nombre = "Estructuras de Datos", Creditos = 4, CarreraId = 1, DocenteId = 1, Modalidad = "Presencial", Sede = "San José", Horario = "Lun/Mié 18:00-20:00", Estado = "Activo", Costo = 180000m },
                new Curso { Id = 2, Codigo = "SC-220", Nombre = "Bases de Datos II", Creditos = 3, CarreraId = 1, Modalidad = "Virtual", Sede = "Online", Horario = "Mar/Jue 19:00-21:00", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 4, Codigo = "SC-310", Nombre = "Programación Avanzada", Creditos = 4, CarreraId = 1, Modalidad = "Presencial", Sede = "San José", Horario = "Lun/Mié 20:00-22:00", Estado = "Activo", Costo = 180000m },
                new Curso { Id = 5, Codigo = "SC-315", Nombre = "Redes de Computadoras", Creditos = 3, CarreraId = 1, Modalidad = "Presencial", Sede = "San José", Horario = "Mar/Jue 18:00-20:00", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 6, Codigo = "SC-402", Nombre = "Ingeniería de Software", Creditos = 4, CarreraId = 1, Modalidad = "Virtual", Sede = "Online", Horario = "Vie 08:00-12:00", Estado = "Activo", Costo = 180000m },

                // Administración de Empresas (CarreraId = 2)
                new Curso { Id = 3, Codigo = "ADM-101", Nombre = "Introducción a la Administración", Creditos = 3, CarreraId = 2, Modalidad = "Presencial", Sede = "San José", Horario = "Vie 08:00-11:00", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 7, Codigo = "ADM-110", Nombre = "Contabilidad General", Creditos = 3, CarreraId = 2, Modalidad = "Presencial", Sede = "Heredia", Horario = "Lun/Mié 18:00-19:30", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 8, Codigo = "ADM-210", Nombre = "Mercadeo", Creditos = 3, CarreraId = 2, Modalidad = "Virtual", Sede = "Online", Horario = "Mar/Jue 19:00-20:30", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 9, Codigo = "ADM-310", Nombre = "Finanzas Corporativas", Creditos = 4, CarreraId = 2, Modalidad = "Presencial", Sede = "San José", Horario = "Sáb 08:00-12:00", Estado = "Activo", Costo = 180000m },

                // Enfermería (CarreraId = 3)
                new Curso { Id = 10, Codigo = "EN-101", Nombre = "Anatomía y Fisiología", Creditos = 4, CarreraId = 3, Modalidad = "Presencial", Sede = "San José", Horario = "Lun/Mié 07:00-09:00", Estado = "Activo", Costo = 180000m },
                new Curso { Id = 11, Codigo = "EN-110", Nombre = "Fundamentos de Enfermería", Creditos = 4, CarreraId = 3, Modalidad = "Presencial", Sede = "San José", Horario = "Mar/Jue 07:00-09:00", Estado = "Activo", Costo = 180000m },
                new Curso { Id = 12, Codigo = "EN-201", Nombre = "Farmacología", Creditos = 3, CarreraId = 3, Modalidad = "Virtual", Sede = "Online", Horario = "Vie 13:00-16:00", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 13, Codigo = "EN-310", Nombre = "Enfermería Materno-Infantil", Creditos = 4, CarreraId = 3, Modalidad = "Presencial", Sede = "Alajuela", Horario = "Lun/Mié 13:00-15:00", Estado = "Activo", Costo = 180000m },
                new Curso { Id = 14, Codigo = "EN-410", Nombre = "Cuidados Críticos", Creditos = 4, CarreraId = 3, Modalidad = "Presencial", Sede = "San José", Horario = "Sáb 07:00-11:00", Estado = "Activo", Costo = 180000m },

                // Derecho (CarreraId = 4)
                new Curso { Id = 15, Codigo = "DE-101", Nombre = "Introducción al Derecho", Creditos = 3, CarreraId = 4, Modalidad = "Presencial", Sede = "San José", Horario = "Lun/Mié 18:00-19:30", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 16, Codigo = "DE-110", Nombre = "Derecho Constitucional", Creditos = 4, CarreraId = 4, Modalidad = "Presencial", Sede = "San José", Horario = "Mar/Jue 18:00-20:00", Estado = "Activo", Costo = 180000m },
                new Curso { Id = 17, Codigo = "DE-201", Nombre = "Derecho Civil I", Creditos = 4, CarreraId = 4, Modalidad = "Virtual", Sede = "Online", Horario = "Vie 18:00-21:00", Estado = "Activo", Costo = 180000m },
                new Curso { Id = 18, Codigo = "DE-301", Nombre = "Derecho Penal", Creditos = 4, CarreraId = 4, Modalidad = "Presencial", Sede = "Heredia", Horario = "Sáb 08:00-12:00", Estado = "Activo", Costo = 180000m },
                new Curso { Id = 19, Codigo = "DE-350", Nombre = "Derecho Laboral", Creditos = 3, CarreraId = 4, Modalidad = "Presencial", Sede = "San José", Horario = "Lun/Mié 20:00-21:30", Estado = "Activo", Costo = 135000m },

                // Educación (CarreraId = 5)
                new Curso { Id = 20, Codigo = "ED-101", Nombre = "Pedagogía General", Creditos = 3, CarreraId = 5, Modalidad = "Presencial", Sede = "San José", Horario = "Lun/Mié 17:00-18:30", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 21, Codigo = "ED-150", Nombre = "Psicología del Aprendizaje", Creditos = 3, CarreraId = 5, Modalidad = "Virtual", Sede = "Online", Horario = "Mar/Jue 17:00-18:30", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 22, Codigo = "ED-210", Nombre = "Didáctica", Creditos = 3, CarreraId = 5, Modalidad = "Presencial", Sede = "Alajuela", Horario = "Vie 14:00-17:00", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 23, Codigo = "ED-310", Nombre = "Evaluación Educativa", Creditos = 3, CarreraId = 5, Modalidad = "Virtual", Sede = "Online", Horario = "Sáb 09:00-12:00", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 24, Codigo = "ED-410", Nombre = "Práctica Docente", Creditos = 4, CarreraId = 5, Modalidad = "Presencial", Sede = "San José", Horario = "Lun/Mié 08:00-10:00", Estado = "Activo", Costo = 180000m },

                // Contabilidad (CarreraId = 6)
                new Curso { Id = 25, Codigo = "CO-101", Nombre = "Contabilidad Básica", Creditos = 4, CarreraId = 6, Modalidad = "Presencial", Sede = "San José", Horario = "Lun/Mié 18:00-20:00", Estado = "Activo", Costo = 180000m },
                new Curso { Id = 26, Codigo = "CO-201", Nombre = "Contabilidad Intermedia", Creditos = 4, CarreraId = 6, Modalidad = "Virtual", Sede = "Online", Horario = "Mar/Jue 18:00-20:00", Estado = "Activo", Costo = 180000m },
                new Curso { Id = 27, Codigo = "CO-250", Nombre = "Costos", Creditos = 3, CarreraId = 6, Modalidad = "Presencial", Sede = "Heredia", Horario = "Vie 18:00-21:00", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 28, Codigo = "CO-310", Nombre = "Auditoría", Creditos = 3, CarreraId = 6, Modalidad = "Presencial", Sede = "San José", Horario = "Sáb 08:00-11:00", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 29, Codigo = "CO-350", Nombre = "Impuestos", Creditos = 3, CarreraId = 6, Modalidad = "Virtual", Sede = "Online", Horario = "Lun/Mié 20:00-21:30", Estado = "Activo", Costo = 135000m }
            );

            modelBuilder.Entity<Matricula>().HasData(
                new Matricula { Id = 1, EstudianteId = 1, CursoId = 1, Fecha = new DateTime(2026, 7, 1), Estado = "Activa" }
            );

            // Pago (transacción) correspondiente a la matrícula de demostración,
            // para que el comprobante/factura tenga datos reales desde el primer momento.
            modelBuilder.Entity<Pago>().HasData(
                new Pago { Id = 1, MatriculaId = 1, Monto = 180000m, FechaPago = new DateTime(2026, 7, 1), NumeroTransaccion = "TXN-00000001", MetodoPago = "Tarjeta de crédito", Estado = "Completado" }
            );
        }

    }
}
