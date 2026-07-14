
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

        public CursosAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cursos = await _context.Cursos
                .Include(c => c.Carrera)
                .Include(c => c.Docente)
                .ToListAsync();

            return View(cursos);
        }

        public IActionResult Create()
        {
            ViewBag.CarreraId = new SelectList(_context.Carreras, "Id", "Nombre");
            ViewBag.DocenteId = new SelectList(_context.Docentes, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Curso curso)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CarreraId = new SelectList(_context.Carreras, "Id", "Nombre");
                ViewBag.DocenteId = new SelectList(_context.Docentes, "Id", "Nombre");
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

            ViewBag.CarreraId = new SelectList(_context.Carreras, "Id", "Nombre", curso.CarreraId);
            ViewBag.DocenteId = new SelectList(_context.Docentes, "Id", "Nombre", curso.DocenteId);

            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Curso curso)
        {
            if (id != curso.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.CarreraId = new SelectList(_context.Carreras, "Id", "Nombre", curso.CarreraId);
                ViewBag.DocenteId = new SelectList(_context.Docentes, "Id", "Nombre", curso.DocenteId);
                return View(curso);
            }

            _context.Update(curso);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Curso actualizado correctamente.";
            return RedirectToAction(nameof(Index));
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
    }
}
