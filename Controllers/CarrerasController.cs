
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

    // GET: CARRERAS
    [AllowAnonymous]
    public async Task<IActionResult> Index()    
    {
        var carreras = await _context.Carreras.ToListAsync();
        return View(carreras);
    }

    // GET: CARRERAS/Details
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

    // GET: CARRERAS/Create
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: CARRERAS/Create

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

    // GET: CARRERAS/Edit
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

    // POST: CARRERAS/Edit

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

    // GET: CARRERAS/Delete
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

    // POST: CARRERAS/Delete
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
