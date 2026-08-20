
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SistemaMatriculaURA.Models;

[Authorize(Roles = "Admin")]
public class CarrerasController : Controller
{
    private readonly ApplicationDbContext _context;

    public CarrerasController(ApplicationDbContext context)
    {
        _context = context;
    }
    [AllowAnonymous]
    public async Task<IActionResult> Index(int page = 1)
    {
        var query = _context.Carreras.OrderBy(c => c.Nombre).AsQueryable();
        var carreras = await PaginatedList<Carrera>.CreateAsync(query, page, 8);
        return View(carreras);
    }

    private const int PageSizePlanEstudios = 5;
    [AllowAnonymous]
    public async Task<IActionResult> PlanEstudios(int? id, int page = 1)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carrera = await _context.Carreras.FirstOrDefaultAsync(c => c.Id == id);
        if (carrera == null)
        {
            return NotFound();
        }

        var query = _context.Cursos
            .Include(c => c.Docente)
            .Where(c => c.CarreraId == id && c.Estado == "Activo")
            .OrderBy(c => c.Nombre)
            .AsQueryable();

        var cursos = await PaginatedList<Curso>.CreateAsync(query, page, PageSizePlanEstudios);

        ViewBag.Carrera = carrera;
        return View(cursos);
    }
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carrera = await _context.Carreras
            .FirstOrDefaultAsync(m => m.Id == id);
        if (carrera == null)
        {
            return NotFound();
        }

        return View(carrera);
    }
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Codigo")] Carrera carrera)
    {
        if (ModelState.IsValid)
        {
            _context.Add(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(carrera);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carrera = await _context.Carreras.FindAsync(id);
        if (carrera == null)
        {
            return NotFound();
        }
        return View(carrera);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,Codigo")] Carrera carrera)
    {
        if (id != carrera.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(carrera);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarreraExists(carrera.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(carrera);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carrera = await _context.Carreras
            .FirstOrDefaultAsync(m => m.Id == id);
        if (carrera == null)
        {
            return NotFound();
        }

        return View(carrera);
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var carrera = await _context.Carreras.FindAsync(id);
        if (carrera != null)
        {
            _context.Carreras.Remove(carrera);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CarreraExists(int? id)
    {
        return _context.Carreras.Any(e => e.Id == id);
    }
}
