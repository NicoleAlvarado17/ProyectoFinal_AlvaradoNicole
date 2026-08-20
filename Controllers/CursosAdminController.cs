
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
        public static readonly string[] Estados = { "Activo", "Congelado", "Inactivo", "Cerrado" };

        public CursosAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Listado con paginación y filtros (créditos, modalidad/tipo, texto libre y
        // "solo sin profesor asignado") para poder ubicar rápido los cursos que
        // necesitan que se les asigne un docente, horario o modalidad.
        public async Task<IActionResult> Index(int page = 1, int? creditos = null, string? modalidad = null, string? busqueda = null, bool soloSinAsignar = false)
        {
            ViewBag.Modalidades = Modalidades;
            ViewBag.CreditosSeleccionado = creditos;
            ViewBag.ModalidadSeleccionada = modalidad;
            ViewBag.Busqueda = busqueda;
            ViewBag.SoloSinAsignar = soloSinAsignar;

            var query = _context.Cursos
                .Include(c => c.Carrera)
                .Include(c => c.Docente)
                .AsQueryable();

            if (creditos.HasValue)
                query = query.Where(c => c.Creditos == creditos.Value);

            if (!string.IsNullOrWhiteSpace(modalidad))
                query = query.Where(c => c.Modalidad == modalidad);

            if (!string.IsNullOrWhiteSpace(busqueda))
                query = query.Where(c => c.Nombre.Contains(busqueda) || c.Codigo.Contains(busqueda));

            if (soloSinAsignar)
                query = query.Where(c => c.DocenteId == null);

            query = query.OrderBy(c => c.Codigo);

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

        // Acción rápida desde Gestión de Cursos: congela el curso (deja de
        // aparecer en el plan de estudios público y en el catálogo del
        // estudiante, sin necesidad de eliminarlo) sin pasar por el formulario
        // completo de Editar.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Congelar(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso != null)
            {
                curso.Estado = "Congelado";
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Curso {curso.Codigo} congelado correctamente.";
            }

            return RedirectToAction(nameof(Index));
        }

        // Revierte un curso congelado (u otro estado) a Activo.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reactivar(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso != null)
            {
                curso.Estado = "Activo";
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Curso {curso.Codigo} reactivado correctamente.";
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
