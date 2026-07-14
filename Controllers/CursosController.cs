using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMatriculaURA.Models;

namespace ProyectoFinal_AlvaradoNicole.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CursosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CursosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTA DE CURSOS
        public async Task<IActionResult> Index()
        {
            var cursos = await _context.Cursos
                .Include(c => c.Carrera)
                .ToListAsync();

            return View(cursos);
        }

        // DETALLES
        public async Task<IActionResult> Details(int id)
        {
            var curso = await _context.Cursos
                .Include(c => c.Carrera)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (curso == null)
                return NotFound();

            return View(curso);
        }

        // CREAR
        public IActionResult Create()
        {
            ViewBag.Carreras = _context.Carreras.ToList();
            ViewBag.Docentes = _context.Docentes.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Curso curso)
        {
            if (ModelState.IsValid)
            {
                _context.Cursos.Add(curso);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Curso creado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Carreras = _context.Carreras.ToList();
            ViewBag.Docentes = _context.Docentes.ToList();
            return View(curso);
        }

        // EDITAR
        public async Task<IActionResult> Edit(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
                return NotFound();

            ViewBag.Carreras = _context.Carreras.ToList();
            ViewBag.Docentes = _context.Docentes.ToList();
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Curso curso)
        {
            if (id != curso.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(curso);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Curso actualizado.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Carreras = _context.Carreras.ToList();
            ViewBag.Docentes = _context.Docentes.ToList();
            return View(curso);
        }

        // ELIMINAR
        public async Task<IActionResult> Delete(int id)
        {
            var curso = await _context.Cursos
                .Include(c => c.Carrera)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (curso == null)
                return NotFound();

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
    }
}
