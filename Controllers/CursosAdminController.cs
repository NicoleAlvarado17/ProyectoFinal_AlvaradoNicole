
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaMatriculaURA.Models;

namespace ProyectoFinal_AlvaradoNicole.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CursosAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int PageSize = 8;

        public static readonly string[] Modalidades = { "Presencial", "Virtual", "Híbrido" };
        public static readonly string[] Sedes = { "San José", "Heredia", "Alajuela", "Online" };
        public static readonly string[] Estados = { "Activo", "Inactivo", "Cerrado" };

        public CursosAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _context.Cursos
                .Include(c => c.Carrera)
                .Include(c => c.Docente)
                .OrderBy(c => c.Codigo)
                .AsQueryable();

            var cursos = await PaginatedList<Curso>.CreateAsync(query, page, PageSize);

            return View(cursos);
        }

        public IActionResult Create()
        {
            CargarListas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Curso curso)
        {
            if (!ModelState.IsValid)
            {
                CargarListas(curso);
                return View(curso);
            }

            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Curso creado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null) return NotFound();

            CargarListas(curso);
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Curso curso)
        {
            if (id != curso.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                CargarListas(curso);
                return View(curso);
            }

            _context.Update(curso);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Curso actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var curso = await _context.Cursos
                .Include(c => c.Carrera)
                .Include(c => c.Docente)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (curso == null) return NotFound();

            return View(curso);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var curso = await _context.Cursos
                .Include(c => c.Carrera)
                .Include(c => c.Docente)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (curso == null) return NotFound();

            return View(curso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);

            if (curso != null)
            {
                _context.Cursos.Remove(curso);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Curso eliminado.";
            }

            return RedirectToAction(nameof(Index));
        }

        private void CargarListas(Curso? curso = null)
        {
            ViewBag.CarreraId = new SelectList(_context.Carreras, "Id", "Nombre", curso?.CarreraId);
            ViewBag.DocenteId = new SelectList(_context.Docentes, "Id", "Nombre", curso?.DocenteId);
            ViewBag.Modalidades = new SelectList(Modalidades, curso?.Modalidad);
            ViewBag.Sedes = new SelectList(Sedes, curso?.Sede);
            ViewBag.Estados = new SelectList(Estados, curso?.Estado ?? "Activo");
        }
    }
}
