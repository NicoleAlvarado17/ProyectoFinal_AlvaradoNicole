using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMatriculaURA.Models;

namespace ProyectoFinal_AlvaradoNicole.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DocentesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocentesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTA DE DOCENTES
        public async Task<IActionResult> Index()
        {
            var docentes = await _context.Docentes.ToListAsync();
            return View(docentes);
        }

        // DETALLES
        public async Task<IActionResult> Details(int id)
        {
            var docente = await _context.Docentes.FirstOrDefaultAsync(d => d.Id == id);

            if (docente == null)
                return NotFound();

            return View(docente);
        }

        // CREAR
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Docente docente)
        {
            if (ModelState.IsValid)
            {
                _context.Docentes.Add(docente);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Profesor registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            return View(docente);
        }

        // EDITAR
        public async Task<IActionResult> Edit(int id)
        {
            var docente = await _context.Docentes.FindAsync(id);
            if (docente == null)
                return NotFound();

            return View(docente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Docente docente)
        {
            if (id != docente.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(docente);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Profesor actualizado.";
                return RedirectToAction(nameof(Index));
            }

            return View(docente);
        }

        // ELIMINAR
        public async Task<IActionResult> Delete(int id)
        {
            var docente = await _context.Docentes.FirstOrDefaultAsync(d => d.Id == id);

            if (docente == null)
                return NotFound();

            return View(docente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var docente = await _context.Docentes.FindAsync(id);

            if (docente != null)
            {
                _context.Docentes.Remove(docente);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Profesor eliminado.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
