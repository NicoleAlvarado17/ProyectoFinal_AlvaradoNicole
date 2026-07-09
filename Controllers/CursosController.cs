
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SistemaMatriculaURA.Models;

[Authorize(Roles = "Admin")]
public class CursosController : Controller
{
    private readonly ApplicationDbContext _context;

    public CursosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: CURSOS
    [AllowAnonymous]
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Cursos.ToListAsync());
    }

    // GET: CURSOS/Details
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var curso = await _context.Cursos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (curso == null)
        {
            return NotFound();
        }

        return View(curso);
    }

    // GET: CURSOS/Create
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: CURSOS/Create

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([Bind("Id,Codigo,Nombre,Creditos,CarreraId,Carrera")] Curso curso)
    {
        if (ModelState.IsValid)
        {
            _context.Add(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(curso);
    }

    // GET: CURSOS/Edit
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null)
        {
            return NotFound();
        }
        return View(curso);
    }

    // POST: CURSOS/Edit

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Codigo,Nombre,Creditos,CarreraId,Carrera")] Curso curso)
    {
        if (id != curso.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(curso);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(curso.Id))
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
        return View(curso);
    }

    // GET: CURSOS/Delete
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var curso = await _context.Cursos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (curso == null)
        {
            return NotFound();
        }

        return View(curso);
    }

    // POST: CURSOS/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso != null)
        {
            _context.Cursos.Remove(curso);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CursoExists(int? id)
    {
        return _context.Cursos.Any(e => e.Id == id);
    }
}
