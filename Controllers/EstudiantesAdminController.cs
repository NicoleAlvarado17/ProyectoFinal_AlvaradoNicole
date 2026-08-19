using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaMatriculaURA.Models;

namespace ProyectoFinal_AlvaradoNicole.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EstudiantesAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstudiantesAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var estudiantes = await _context.Estudiantes
                .Include(e => e.Carrera)
                .ToListAsync();

            return View(estudiantes);
        }

        public IActionResult Create()
        {
            ViewBag.CarreraId = new SelectList(_context.Carreras, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estudiante estudiante)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CarreraId = new SelectList(_context.Carreras, "Id", "Nombre");
                return View(estudiante);
            }

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Estudiante registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null) return NotFound();

            ViewBag.CarreraId = new SelectList(_context.Carreras, "Id", "Nombre", estudiante.CarreraId);
            return View(estudiante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Estudiante estudiante)
        {
            if (id != estudiante.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.CarreraId = new SelectList(_context.Carreras, "Id", "Nombre", estudiante.CarreraId);
                return View(estudiante);
            }

            _context.Update(estudiante);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Estudiante actualizado.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var estudiante = await _context.Estudiantes
                .Include(e => e.Carrera)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estudiante == null) return NotFound();

            return View(estudiante);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var estudiante = await _context.Estudiantes
                .Include(e => e.Carrera)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estudiante == null) return NotFound();

            return View(estudiante);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);

            if (estudiante != null)
            {
                var tieneMatriculas = await _context.Matriculas.AnyAsync(m => m.EstudianteId == estudiante.Id);

                if (tieneMatriculas)
                {
                    TempData["Error"] = "No se puede eliminar el estudiante porque tiene matrículas asociadas.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Estudiante eliminado.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
// Prueba para pull request