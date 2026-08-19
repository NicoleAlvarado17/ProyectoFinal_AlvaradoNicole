using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_AlvaradoNicole.Models;
using SistemaMatriculaURA.Models;
using System.Diagnostics;

namespace ProyectoFinal_AlvaradoNicole.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        // Página principal
        public IActionResult Index()
        {
            return View();
        }

        // Dashboard Estudiante (HU07/HU10/HU11 - datos reales del estudiante autenticado)
        [Authorize(Roles = "Estudiante")]
        public async Task<IActionResult> Dashboard()
        {
            var userId = _userManager.GetUserId(User);
            var email = User.Identity?.Name;

            var estudiante = await _context.Estudiantes
                .Include(e => e.Carrera)
                .FirstOrDefaultAsync(e => e.UserId == userId || e.Correo == email);

            if (estudiante == null)
            {
                // Aún no tiene registro en Estudiantes (se crea al visitar el catálogo de cursos).
                return View(new EstudianteDashboardViewModel { NombreCarrera = "Sin carrera asignada" });
            }

            var matriculasActivas = await _context.Matriculas
                .Include(m => m.Curso)
                .Where(m => m.EstudianteId == estudiante.Id && m.Estado == "Activa")
                .ToListAsync();

            var vm = new EstudianteDashboardViewModel
            {
                NombreCarrera = estudiante.Carrera?.Nombre ?? "Sin carrera asignada",
                CreditosMatriculados = matriculasActivas.Sum(m => m.Curso.Creditos),
                CursosActivos = matriculasActivas.Count,
                Cursos = matriculasActivas.Select(m => new EstudianteDashboardCursoViewModel
                {
                    Nombre = m.Curso.Nombre,
                    Modalidad = m.Curso.Modalidad,
                    Estado = m.Estado
                }).ToList()
            };

            return View(vm);
        }

        // Dashboard Docente: la vista real vive en PanelDocenteController (busca al
        // docente por correo). Se conserva esta ruta como redirección de respaldo
        // porque Login.cshtml.cs y Register.cshtml.cs pueden apuntar aquí.
        [Authorize(Roles = "Docente")]
        public IActionResult DashboardDocente()
        {
            return RedirectToAction("DashboardDocente", "PanelDocente");
       }

        // Página de privacidad
        public IActionResult Privacy()
        {
            return View();
        }

        // Manejo de errores
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}