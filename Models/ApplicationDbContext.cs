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
                new Curso { Id = 1, Codigo = "SC-301", Nombre = "Estructuras de Datos", Creditos = 4, CarreraId = 1, DocenteId = 1, Modalidad = "Presencial", Sede = "San José", Horario = "Lun/Mié 18:00-20:00", Grupo = "A", Aula = "Aula 204", Estado = "Activo", Costo = 180000m },
                new Curso { Id = 2, Codigo = "SC-220", Nombre = "Bases de Datos II", Creditos = 3, CarreraId = 1, DocenteId = 1, Modalidad = "Virtual", Sede = "Online", Horario = "Mar/Jue 19:00-21:00", Grupo = "B", Aula = "Sala Virtual 1", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 4, Codigo = "SC-310", Nombre = "Programación Avanzada", Creditos = 4, CarreraId = 1, Modalidad = "Presencial", Sede = "San José", Horario = "Lun/Mié 20:00-22:00", Estado = "Activo", Costo = 180000m },
                new Curso { Id = 5, Codigo = "SC-315", Nombre = "Redes de Computadoras", Creditos = 3, CarreraId = 1, Modalidad = "Presencial", Sede = "San José", Horario = "Mar/Jue 18:00-20:00", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 6, Codigo = "SC-402", Nombre = "Ingeniería de Software", Creditos = 4, CarreraId = 1, DocenteId = 1, Modalidad = "Virtual", Sede = "Online", Horario = "Vie 08:00-12:00", Grupo = "A", Aula = "Sala Virtual 2", Estado = "Activo", Costo = 180000m },

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

            // Cursos adicionales (ampliación del plan de estudios, ficticios) para
            // tener listados más largos y poder demostrar paginación y filtros
            // (créditos, modalidad, profesor sin asignar) tanto en el catálogo del
            // estudiante como en la Gestión de Cursos del administrador.
            modelBuilder.Entity<Curso>().HasData(
                // Ingeniería en Sistemas (CarreraId = 1)
                new Curso { Id = 30, Codigo = "SC-410", Nombre = "Inteligencia Artificial", Creditos = 4, CarreraId = 1, Modalidad = "Híbrido", Sede = "San José", Horario = "Sáb 13:00-17:00", Estado = "Activo", Costo = 180000m },
                new Curso { Id = 31, Codigo = "SC-215", Nombre = "Sistemas Operativos", Creditos = 3, CarreraId = 1, DocenteId = 1, Modalidad = "Presencial", Sede = "San José", Horario = "Lun/Mié 07:00-09:00", Grupo = "C", Aula = "Aula 210", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 32, Codigo = "SC-150", Nombre = "Introducción a la Programación", Creditos = 2, CarreraId = 1, Modalidad = "Virtual", Sede = "Online", Horario = "Mar/Jue 07:00-08:30", Estado = "Activo", Costo = 90000m },

                // Administración de Empresas (CarreraId = 2)
                new Curso { Id = 33, Codigo = "ADM-320", Nombre = "Gestión de Proyectos", Creditos = 3, CarreraId = 2, Modalidad = "Híbrido", Sede = "Heredia", Horario = "Vie 17:00-20:00", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 34, Codigo = "ADM-150", Nombre = "Matemática Financiera", Creditos = 2, CarreraId = 2, Modalidad = "Presencial", Sede = "San José", Horario = "Sáb 08:00-10:00", Estado = "Activo", Costo = 90000m },
                new Curso { Id = 35, Codigo = "ADM-410", Nombre = "Comercio Internacional", Creditos = 4, CarreraId = 2, Modalidad = "Virtual", Sede = "Online", Horario = "Lun/Mié 19:00-21:00", Estado = "Activo", Costo = 180000m },

                // Enfermería (CarreraId = 3)
                new Curso { Id = 36, Codigo = "EN-150", Nombre = "Nutrición Clínica", Creditos = 2, CarreraId = 3, Modalidad = "Virtual", Sede = "Online", Horario = "Mar 14:00-16:00", Estado = "Activo", Costo = 90000m },
                new Curso { Id = 37, Codigo = "EN-320", Nombre = "Salud Pública", Creditos = 3, CarreraId = 3, Modalidad = "Híbrido", Sede = "Alajuela", Horario = "Jue 13:00-16:00", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 38, Codigo = "EN-450", Nombre = "Enfermería Quirúrgica", Creditos = 4, CarreraId = 3, Modalidad = "Presencial", Sede = "San José", Horario = "Sáb 13:00-17:00", Estado = "Activo", Costo = 180000m },

                // Derecho (CarreraId = 4)
                new Curso { Id = 39, Codigo = "DE-150", Nombre = "Derecho Romano", Creditos = 2, CarreraId = 4, Modalidad = "Presencial", Sede = "San José", Horario = "Mar 18:00-19:30", Estado = "Activo", Costo = 90000m },
                new Curso { Id = 40, Codigo = "DE-410", Nombre = "Derecho Internacional", Creditos = 4, CarreraId = 4, Modalidad = "Híbrido", Sede = "Heredia", Horario = "Vie 18:00-21:00", Estado = "Activo", Costo = 180000m },
                new Curso { Id = 41, Codigo = "DE-320", Nombre = "Derecho Mercantil", Creditos = 3, CarreraId = 4, Modalidad = "Virtual", Sede = "Online", Horario = "Jue 18:00-20:30", Estado = "Activo", Costo = 135000m },

                // Educación (CarreraId = 5)
                new Curso { Id = 42, Codigo = "ED-160", Nombre = "Tecnología Educativa", Creditos = 2, CarreraId = 5, Modalidad = "Virtual", Sede = "Online", Horario = "Mar 16:00-17:30", Estado = "Activo", Costo = 90000m },
                new Curso { Id = 43, Codigo = "ED-320", Nombre = "Gestión de Aula", Creditos = 3, CarreraId = 5, Modalidad = "Híbrido", Sede = "San José", Horario = "Sáb 09:00-12:00", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 44, Codigo = "ED-450", Nombre = "Necesidades Educativas Especiales", Creditos = 4, CarreraId = 5, Modalidad = "Presencial", Sede = "Alajuela", Horario = "Lun/Mié 15:00-17:00", Estado = "Activo", Costo = 180000m },

                // Contabilidad (CarreraId = 6)
                new Curso { Id = 45, Codigo = "CO-150", Nombre = "Matemática Financiera Contable", Creditos = 2, CarreraId = 6, Modalidad = "Presencial", Sede = "San José", Horario = "Mar 07:00-08:30", Estado = "Activo", Costo = 90000m },
                new Curso { Id = 46, Codigo = "CO-410", Nombre = "Contabilidad de Costos Avanzada", Creditos = 4, CarreraId = 6, Modalidad = "Híbrido", Sede = "Heredia", Horario = "Sáb 13:00-17:00", Estado = "Activo", Costo = 180000m },
                new Curso { Id = 47, Codigo = "CO-320", Nombre = "Normas Internacionales (NIIF)", Creditos = 3, CarreraId = 6, Modalidad = "Virtual", Sede = "Online", Horario = "Jue 19:00-21:00", Estado = "Activo", Costo = 135000m }
            );

            // Segunda ampliación: completa exactamente 10 cursos por carrera
            // (para que el plan de estudios de cada carrera pagine de forma
            // uniforme, con 5 cursos por página).
            modelBuilder.Entity<Curso>().HasData(
                new Curso { Id = 48, Codigo = "SC-330", Nombre = "Arquitectura de Software", Creditos = 3, CarreraId = 1, Modalidad = "Presencial", Sede = "San José", Horario = "Jue 18:00-20:00", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 49, Codigo = "SC-360", Nombre = "Seguridad Informática", Creditos = 3, CarreraId = 1, DocenteId = 1, Modalidad = "Virtual", Sede = "Online", Horario = "Sáb 09:00-11:00", Grupo = "B", Aula = "Sala Virtual 3", Estado = "Activo", Costo = 135000m },

                new Curso { Id = 50, Codigo = "ADM-220", Nombre = "Recursos Humanos", Creditos = 3, CarreraId = 2, Modalidad = "Presencial", Sede = "San José", Horario = "Mar 18:00-19:30", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 51, Codigo = "ADM-350", Nombre = "Logística y Cadena de Suministro", Creditos = 3, CarreraId = 2, Modalidad = "Híbrido", Sede = "Heredia", Horario = "Jue 17:00-19:30", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 52, Codigo = "ADM-450", Nombre = "Emprendimiento", Creditos = 2, CarreraId = 2, Modalidad = "Virtual", Sede = "Online", Horario = "Vie 19:00-20:30", Estado = "Activo", Costo = 90000m },

                new Curso { Id = 53, Codigo = "EN-250", Nombre = "Salud Mental", Creditos = 3, CarreraId = 3, Modalidad = "Presencial", Sede = "San José", Horario = "Mié 14:00-16:30", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 54, Codigo = "EN-360", Nombre = "Enfermería Comunitaria", Creditos = 3, CarreraId = 3, Modalidad = "Híbrido", Sede = "Alajuela", Horario = "Vie 09:00-11:30", Estado = "Activo", Costo = 135000m },

                new Curso { Id = 55, Codigo = "DE-360", Nombre = "Derecho Administrativo", Creditos = 3, CarreraId = 4, Modalidad = "Presencial", Sede = "San José", Horario = "Mié 18:00-20:30", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 56, Codigo = "DE-420", Nombre = "Derecho Tributario", Creditos = 4, CarreraId = 4, Modalidad = "Virtual", Sede = "Online", Horario = "Sáb 09:00-13:00", Estado = "Activo", Costo = 180000m },

                new Curso { Id = 57, Codigo = "ED-250", Nombre = "Currículo Educativo", Creditos = 3, CarreraId = 5, Modalidad = "Presencial", Sede = "San José", Horario = "Mié 17:00-19:30", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 58, Codigo = "ED-360", Nombre = "Educación Inclusiva", Creditos = 3, CarreraId = 5, Modalidad = "Virtual", Sede = "Online", Horario = "Vie 16:00-18:30", Estado = "Activo", Costo = 135000m },

                new Curso { Id = 59, Codigo = "CO-260", Nombre = "Contabilidad Gubernamental", Creditos = 3, CarreraId = 6, Modalidad = "Presencial", Sede = "San José", Horario = "Jue 18:00-20:30", Estado = "Activo", Costo = 135000m },
                new Curso { Id = 60, Codigo = "CO-420", Nombre = "Auditoría Avanzada", Creditos = 4, CarreraId = 6, Modalidad = "Híbrido", Sede = "Heredia", Horario = "Sáb 09:00-13:00", Estado = "Activo", Costo = 180000m }
            );

            // Matrículas del estudiante de demostración (Nicole Vargas Solano, Id 1):
            // 4 cursos activos del cuatrimestre actual (II-2026) + 6 cursos ya
            // finalizados en cuatrimestres anteriores (con Nota) para poblar el
            // Historial Académico (HU11) y las métricas del Dashboard Estudiante
            // (promedio ponderado y progreso de carrera - HU07/HU10).
            //
            // IMPORTANTE: se usan Id altos (9001+) a propósito, en vez de 2,3,4...
            // La columna Id es IDENTITY y ya existen filas reales creadas por la
            // aplicación (matrículas hechas al probar "Matricular" en el catálogo),
            // así que usar Id bajos aquí choca con esas filas reales al aplicar la
            // migración (Violation of PRIMARY KEY constraint).
            modelBuilder.Entity<Matricula>().HasData(
                // Matrícula original (ya sembrada desde la migración inicial): solo
                // se le agrega Cuatrimestre, no cambia de Id.
                new Matricula { Id = 1, EstudianteId = 1, CursoId = 1, Fecha = new DateTime(2026, 7, 1), Estado = "Activa", Cuatrimestre = "II-2026" },

                // Cuatrimestre actual (Activa, sin nota todavía)
                new Matricula { Id = 9001, EstudianteId = 1, CursoId = 2, Fecha = new DateTime(2026, 7, 1), Estado = "Activa", Cuatrimestre = "II-2026" },
                new Matricula { Id = 9002, EstudianteId = 1, CursoId = 30, Fecha = new DateTime(2026, 7, 1), Estado = "Activa", Cuatrimestre = "II-2026" },
                new Matricula { Id = 9003, EstudianteId = 1, CursoId = 49, Fecha = new DateTime(2026, 7, 1), Estado = "Activa", Cuatrimestre = "II-2026" },

                // Historial académico (cursos finalizados en cuatrimestres previos)
                new Matricula { Id = 9004, EstudianteId = 1, CursoId = 32, Fecha = new DateTime(2025, 2, 1), Estado = "Aprobada", Cuatrimestre = "I-2025", Nota = 92 },
                new Matricula { Id = 9005, EstudianteId = 1, CursoId = 4, Fecha = new DateTime(2025, 2, 1), Estado = "Aprobada", Cuatrimestre = "I-2025", Nota = 78 },
                new Matricula { Id = 9006, EstudianteId = 1, CursoId = 5, Fecha = new DateTime(2025, 8, 1), Estado = "Reprobada", Cuatrimestre = "II-2025", Nota = 65 },
                new Matricula { Id = 9007, EstudianteId = 1, CursoId = 48, Fecha = new DateTime(2025, 8, 1), Estado = "Aprobada", Cuatrimestre = "II-2025", Nota = 85 },
                new Matricula { Id = 9008, EstudianteId = 1, CursoId = 6, Fecha = new DateTime(2025, 8, 1), Estado = "Aprobada", Cuatrimestre = "II-2025", Nota = 90 },
                new Matricula { Id = 9009, EstudianteId = 1, CursoId = 31, Fecha = new DateTime(2026, 2, 1), Estado = "Aprobada", Cuatrimestre = "I-2026", Nota = 88 }
            );

            // Pagos (transacciones) correspondientes a las matrículas activas del
            // cuatrimestre actual, para que el comprobante/factura tenga datos
            // reales desde el primer momento. Mismo motivo que arriba: Id altos
            // (9001+) para no chocar con pagos reales ya creados por la app.
            modelBuilder.Entity<Pago>().HasData(
                // Pago original (ya sembrado desde la migración inicial), sin cambios.
                new Pago { Id = 1, MatriculaId = 1, Monto = 180000m, FechaPago = new DateTime(2026, 7, 1), NumeroTransaccion = "TXN-00000001", MetodoPago = "Tarjeta de crédito", Estado = "Completado" },

                new Pago { Id = 9001, MatriculaId = 9001, Monto = 135000m, FechaPago = new DateTime(2026, 7, 1), NumeroTransaccion = "TXN-00009001", MetodoPago = "Tarjeta de crédito", Estado = "Completado" },
                new Pago { Id = 9002, MatriculaId = 9002, Monto = 180000m, FechaPago = new DateTime(2026, 7, 1), NumeroTransaccion = "TXN-00009002", MetodoPago = "Tarjeta de crédito", Estado = "Completado" },
                new Pago { Id = 9003, MatriculaId = 9003, Monto = 135000m, FechaPago = new DateTime(2026, 7, 1), NumeroTransaccion = "TXN-00009003", MetodoPago = "Tarjeta de crédito", Estado = "Completado" }
            );

            // Registros de asistencia (HU14) del estudiante de demostración en sus
            // cursos activos, usados para calcular el "Progreso del cuatrimestre"
            // (porcentaje de asistencia por materia) en el Dashboard Estudiante.
            // Mismo motivo que arriba: Id altos (9001+) para no chocar con
            // asistencias reales ya guardadas por un docente desde el Panel Docente.
            modelBuilder.Entity<Asistencia>().HasData(
                // SC-301 Estructuras de Datos (Lun/Mié) - 3 de 4 presentes (75%)
                new Asistencia { Id = 9001, CursoId = 1, EstudianteId = 1, Fecha = new DateTime(2026, 8, 3), Presente = true },
                new Asistencia { Id = 9002, CursoId = 1, EstudianteId = 1, Fecha = new DateTime(2026, 8, 5), Presente = true },
                new Asistencia { Id = 9003, CursoId = 1, EstudianteId = 1, Fecha = new DateTime(2026, 8, 10), Presente = true },
                new Asistencia { Id = 9004, CursoId = 1, EstudianteId = 1, Fecha = new DateTime(2026, 8, 12), Presente = false },

                // SC-220 Bases de Datos II (Mar/Jue) - 4 de 5 presentes (80%)
                new Asistencia { Id = 9005, CursoId = 2, EstudianteId = 1, Fecha = new DateTime(2026, 8, 4), Presente = true },
                new Asistencia { Id = 9006, CursoId = 2, EstudianteId = 1, Fecha = new DateTime(2026, 8, 6), Presente = true },
                new Asistencia { Id = 9007, CursoId = 2, EstudianteId = 1, Fecha = new DateTime(2026, 8, 11), Presente = true },
                new Asistencia { Id = 9008, CursoId = 2, EstudianteId = 1, Fecha = new DateTime(2026, 8, 13), Presente = true },
                new Asistencia { Id = 9009, CursoId = 2, EstudianteId = 1, Fecha = new DateTime(2026, 8, 18), Presente = false },

                // SC-410 Inteligencia Artificial (Sáb) - 1 de 2 presentes (50%)
                new Asistencia { Id = 9010, CursoId = 30, EstudianteId = 1, Fecha = new DateTime(2026, 8, 8), Presente = true },
                new Asistencia { Id = 9011, CursoId = 30, EstudianteId = 1, Fecha = new DateTime(2026, 8, 15), Presente = false },

                // SC-360 Seguridad Informática (Sáb) - 3 de 3 presentes (100%)
                new Asistencia { Id = 9012, CursoId = 49, EstudianteId = 1, Fecha = new DateTime(2026, 8, 1), Presente = true },
                new Asistencia { Id = 9013, CursoId = 49, EstudianteId = 1, Fecha = new DateTime(2026, 8, 8), Presente = true },
                new Asistencia { Id = 9014, CursoId = 49, EstudianteId = 1, Fecha = new DateTime(2026, 8, 15), Presente = true }
            );
        }

    }
}
