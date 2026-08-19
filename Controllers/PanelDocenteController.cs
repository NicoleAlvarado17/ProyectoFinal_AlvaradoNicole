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
        public async Task<IActionResult> Asistencia(int cursoId, DateTime? fecha)
        {
            var curso = await _context.Cursos.FindAsync(cursoId);
            if (curso == null)
                return NotFound();

            if (curso.Modalidad.ToLower() != "presencial")
                return Content("Este curso es virtual. No requiere asistencia.");

            var fechaSesion = (fecha ?? DateTime.Today).Date;

            var matriculas = await _context.Matriculas
                .Include(m => m.Estudiante)
                .Where(m => m.CursoId == cursoId && m.Estado == "Activa")
                .ToListAsync();

            var asistenciasGuardadas = await _context.Asistencias
                .Where(a => a.CursoId == cursoId && a.Fecha == fechaSesion)
                .ToDictionaryAsync(a => a.EstudianteId, a => a.Presente);

            var vm = matriculas.Select(m => new AsistenciaEstudianteViewModel
            {
                EstudianteId = m.EstudianteId,
                Nombre = m.Estudiante.Nombre,
                Presente = asistenciasGuardadas.TryGetValue(m.EstudianteId, out var presente) && presente
            }).OrderBy(x => x.Nombre).ToList();

            ViewBag.Curso = curso;
            ViewBag.Fecha = fechaSesion;

            return View(vm);
        }

        // Guarda la asistencia de la sesión mediante AJAX (sin recargar la página).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardarAsistencia([FromBody] GuardarAsistenciaRequest request)
        {
            if (request == null || request.CursoId <= 0)
                return BadRequest(new { ok = false, mensaje = "Solicitud inválida." });

            var curso = await _context.Cursos.FindAsync(request.CursoId);
            if (curso == null)
                return NotFound(new { ok = false, mensaje = "Curso no encontrado." });

            var fechaSesion = request.Fecha.Date;

            var existentes = await _context.Asistencias
                .Where(a => a.CursoId == request.CursoId && a.Fecha == fechaSesion)
                .ToListAsync();

            foreach (var item in request.Estudiantes ?? new List<AsistenciaItemDto>())
            {
                var registro = existentes.FirstOrDefault(a => a.EstudianteId == item.EstudianteId);

                if (registro == null)
                {
                    _context.Asistencias.Add(new Asistencia
                    {
                        CursoId = request.CursoId,
                        EstudianteId = item.EstudianteId,
                        Fecha = fechaSesion,
                        Presente = item.Presente
                    });
                }
                else
                {
                    registro.Presente = item.Presente;
                }
            }

            await _context.SaveChangesAsync();

            return Json(new { ok = true, mensaje = "Asistencia guardada correctamente." });
        }
    }

    public class AsistenciaEstudianteViewModel
    {
        public int EstudianteId { get; set; }
        public string Nombre { get; set; } = "";
        public bool Presente { get; set; }
    }

    public class GuardarAsistenciaRequest
    {
        public int CursoId { get; set; }
        public DateTime Fecha { get; set; }
        public List<AsistenciaItemDto> Estudiantes { get; set; } = new();
    }

    public class AsistenciaItemDto
    {
        public int EstudianteId { get; set; }
        public bool Presente { get; set; }
    }
}
