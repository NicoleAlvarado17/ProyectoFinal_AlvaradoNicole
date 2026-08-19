using Microsoft.EntityFrameworkCore;
using SistemaMatriculaURA.Models;
using Xunit;

namespace ProyectoFinal_AlvaradoNicole.Tests
{
    // Pruebas de integración ligeras contra una base de datos en memoria: no
    // reemplazan probar con SQL Server real, pero verifican que el modelo de
    // datos (relaciones, claves foráneas, datos semilla) se construye
    // correctamente y que las operaciones básicas de las historias de usuario
    // (matricular un curso, registrar asistencia) funcionan de extremo a extremo.
    public class ApplicationDbContextTests
    {
        private static ApplicationDbContext CrearContexto(string nombreBd)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(nombreBd)
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task LosDatosSemillaSeCargan()
        {
            using var context = CrearContexto(nameof(LosDatosSemillaSeCargan));
            await context.Database.EnsureCreatedAsync();

            Assert.True(await context.Carreras.AnyAsync(c => c.Codigo == "ING-SIS"));
            Assert.True(await context.Docentes.AnyAsync(d => d.Correo == "docente1@ura.com"));
            Assert.True(await context.Estudiantes.AnyAsync(e => e.Correo == "estudiante1@ura.com"));
        }

        [Fact]
        public async Task SePuedeMatricularUnEstudianteEnUnCurso()
        {
            using var context = CrearContexto(nameof(SePuedeMatricularUnEstudianteEnUnCurso));
            await context.Database.EnsureCreatedAsync();

            var carrera = new Carrera { Nombre = "Carrera de prueba", Codigo = "PRB-001" };
            context.Carreras.Add(carrera);
            await context.SaveChangesAsync();

            var estudiante = new Estudiante { Nombre = "Estudiante de prueba", Correo = "prueba@ura.com", CarreraId = carrera.Id };
            context.Estudiantes.Add(estudiante);

            var curso = new Curso { Codigo = "PR-100", Nombre = "Curso de prueba", Creditos = 3, CarreraId = carrera.Id, Estado = "Activo" };
            context.Cursos.Add(curso);
            await context.SaveChangesAsync();

            context.Matriculas.Add(new Matricula
            {
                EstudianteId = estudiante.Id,
                CursoId = curso.Id,
                Fecha = DateTime.Today,
                Estado = "Activa"
            });
            await context.SaveChangesAsync();

            var matriculasDelEstudiante = await context.Matriculas
                .Where(m => m.EstudianteId == estudiante.Id)
                .CountAsync();

            Assert.Equal(1, matriculasDelEstudiante);
        }

        [Fact]
        public async Task SePuedeRegistrarAsistenciaDeUnCursoPresencial()
        {
            using var context = CrearContexto(nameof(SePuedeRegistrarAsistenciaDeUnCursoPresencial));
            await context.Database.EnsureCreatedAsync();

            // Curso Id=1 y Estudiante Id=1 vienen de los datos semilla (HU14).
            context.Asistencias.Add(new Asistencia
            {
                CursoId = 1,
                EstudianteId = 1,
                Fecha = new DateTime(2026, 8, 19),
                Presente = true
            });
            await context.SaveChangesAsync();

            var asistencia = await context.Asistencias
                .FirstOrDefaultAsync(a => a.CursoId == 1 && a.EstudianteId == 1);

            Assert.NotNull(asistencia);
            Assert.True(asistencia!.Presente);
        }
    }
}
