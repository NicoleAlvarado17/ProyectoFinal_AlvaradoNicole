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
        public async Task<IActionResult> Index()
        {
            var carreras = await _context.Carreras.OrderBy(c => c.Nombre).ToListAsync();
            return View(carreras);
        }
        [Authorize(Roles = "Estudiante")]
        public async Task<IActionResult> Dashboard()
        {
            var userId = _userManager.GetUserId(User);
            var email = User.Identity?.Name;
            var appUser = await _userManager.GetUserAsync(User);

            var estudiante = await _context.Estudiantes
                .Include(e => e.Carrera)
                .FirstOrDefaultAsync(e => e.UserId == userId || e.Correo == email);

            if (estudiante == null)
            {
                return View(new EstudianteDashboardViewModel
                {
                    NombreEstudiante = appUser?.FullName ?? email ?? "Estudiante",
                    NombreCarrera = "Sin carrera asignada"
                });
            }

            var matriculasActivas = await _context.Matriculas
                .Include(m => m.Curso)
                .Where(m => m.EstudianteId == estudiante.Id && m.Estado == "Activa")
                .ToListAsync();
            var matriculasFinalizadas = await _context.Matriculas
                .Include(m => m.Curso)
                .Where(m => m.EstudianteId == estudiante.Id && m.Nota != null)
                .ToListAsync();

            double? promedioGeneral = null;
            var creditosParaPromedio = matriculasFinalizadas.Sum(m => m.Curso.Creditos);
            if (creditosParaPromedio > 0)
            {
                promedioGeneral = matriculasFinalizadas.Sum(m => m.Nota!.Value * m.Curso.Creditos) / (double)creditosParaPromedio;
            }

            var creditosAprobados = matriculasFinalizadas
                .Where(m => m.Estado == "Aprobada")
                .Sum(m => m.Curso.Creditos);

            var creditosCarreraTotales = await _context.Cursos
                .Where(c => c.CarreraId == estudiante.CarreraId && c.Estado == "Activo")
                .SumAsync(c => c.Creditos);

            double progresoCarrera = creditosCarreraTotales > 0
                ? Math.Round(creditosAprobados * 100.0 / creditosCarreraTotales, 0)
                : 0;
            var cursoIdsActivos = matriculasActivas.Select(m => m.CursoId).ToList();
            var asistenciasActivas = await _context.Asistencias
                .Where(a => a.EstudianteId == estudiante.Id && cursoIdsActivos.Contains(a.CursoId))
                .ToListAsync();

            var progresoCursos = matriculasActivas.Select(m =>
            {
                var registros = asistenciasActivas.Where(a => a.CursoId == m.CursoId).ToList();
                double? porcentaje = registros.Any()
                    ? Math.Round(registros.Count(a => a.Presente) * 100.0 / registros.Count, 0)
                    : null;

                return new EstudianteDashboardProgresoCursoViewModel
                {
                    Nombre = m.Curso.Nombre,
                    PorcentajeAsistencia = porcentaje
                };
            }).ToList();

            var vm = new EstudianteDashboardViewModel
            {
                NombreEstudiante = appUser?.FullName ?? estudiante.Nombre,
                NombreCarrera = estudiante.Carrera?.Nombre ?? "Sin carrera asignada",
                CreditosMatriculados = matriculasActivas.Sum(m => m.Curso.Creditos),
                CursosActivos = matriculasActivas.Count,
                Cursos = matriculasActivas.Select(m => new EstudianteDashboardCursoViewModel
                {
                    Nombre = m.Curso.Nombre,
                    Modalidad = m.Curso.Modalidad,
                    Estado = m.Estado
                }).ToList(),
                PromedioGeneral = promedioGeneral,
                CreditosCarreraCompletados = creditosAprobados,
                CreditosCarreraTotales = creditosCarreraTotales,
                ProgresoCarreraPorcentaje = progresoCarrera,
                ProgresoCursos = progresoCursos
            };

            return View(vm);
        }
        [Authorize(Roles = "Docente")]
        public IActionResult DashboardDocente()
        {
            return RedirectToAction("DashboardDocente", "PanelDocente");
       }
        public IActionResult Privacy()
        {
            return View();
        }
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