using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMatriculaURA.Models;

namespace ProyectoFinal_AlvaradoNicole.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MatriculasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatriculasController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Auditoria(int? estudianteId, int? cursoId, string estado)
        {
            var query = _context.Matriculas
                .Include(m => m.Estudiante)
                .Include(m => m.Curso)
                .ThenInclude(c => c.Carrera)
                .AsQueryable();
            if (estudianteId.HasValue)
                query = query.Where(m => m.EstudianteId == estudianteId.Value);

            if (cursoId.HasValue)
                query = query.Where(m => m.CursoId == cursoId.Value);

            if (!string.IsNullOrEmpty(estado))
                query = query.Where(m => m.Estado == estado);
            ViewBag.Estudiantes = _context.Estudiantes.OrderBy(e => e.Nombre).ToList();
            ViewBag.Cursos = _context.Cursos.OrderBy(c => c.Nombre).ToList();
            ViewBag.Estados = new List<string> { "Activa", "Congelada", "Retirada" };

            return View(await query.ToListAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            var matricula = await _context.Matriculas
                .Include(m => m.Estudiante)
                .Include(m => m.Curso)
                .ThenInclude(c => c.Carrera)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (matricula == null)
                return NotFound();

            return View(matricula);
        }
    }
}
