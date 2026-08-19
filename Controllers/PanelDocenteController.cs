using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMatriculaURA.Models;

namespace ProyectoFinal_AlvaradoNicole.Controllers
{
    [Authorize(Roles = "Docente")]
    public class PanelDocenteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PanelDocenteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // DASHBOARD
        public async Task<IActionResult> DashboardDocente()
        {
            string correo = User.Identity.Name;

            var docente = await _context.Docentes
                .FirstOrDefaultAsync(d => d.Correo == correo);

            if (docente == null)
                return Content("No se encontró el docente asociado a este usuario.");

            var cursos = await _context.Cursos
                .Where(c => c.DocenteId == docente.Id)
                .ToListAsync();

            int totalEstudiantes = await _context.Matriculas
                .Where(m => cursos.Select(c => c.Id).Contains(m.CursoId))
                .CountAsync();

            ViewBag.Cursos = cursos.Count;
            ViewBag.Estudiantes = totalEstudiantes;

            return View(docente);
        }

        // HU12 – CARGA ACADÉMICA
        public async Task<IActionResult> CargaAcademica()
        {
            string correo = User.Identity.Name;

            var docente = await _context.Docentes
                .FirstOrDefaultAsync(d => d.Correo == correo);

            var cursos = await _context.Cursos
                .Where(c => c.DocenteId == docente.Id)
                .ToListAsync();

            return View(cursos);
        }

        // HU13 – LISTA DE CLASE
        public async Task<IActionResult> ListaClase(int cursoId)
        {
            var matriculas = await _context.Matriculas
                .Include(m => m.Estudiante)
                .Where(m => m.CursoId == cursoId)
                .ToListAsync();

            var curso = await _context.Cursos.FindAsync(cursoId);

            ViewBag.Curso = curso;

            return View(matriculas);
        }

        // HU14 – CONTROL DE ASISTENCIA
        public async Task<IActionResult> Asistencia(int cursoId)
        {
            var curso = await _context.Cursos.FindAsync(cursoId);

            if (curso.Modalidad.ToLower() != "presencial")
                return Content("Este curso es virtual. No requiere asistencia.");

            var matriculas = await _context.Matriculas
                .Include(m => m.Estudiante)
                .Where(m => m.CursoId == cursoId)
                .ToListAsync();

            ViewBag.Curso = curso;

            return View(matriculas);
        }
    }
}
