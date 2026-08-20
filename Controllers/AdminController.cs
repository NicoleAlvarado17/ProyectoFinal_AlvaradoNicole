using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMatriculaURA.Models;

namespace ProyectoFinal_AlvaradoNicole.Controllers
{
    // Panel de administración: pantalla de aterrizaje del rol Admin al iniciar
    // sesión, con el resumen general del sistema y accesos directos a los
    // módulos de gestión (igual que el prototipo de diseño, Pantalla 11).
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var vm = new AdminDashboardViewModel
            {
                TotalEstudiantes = await _context.Estudiantes.CountAsync(),
                TotalProfesores = await _context.Docentes.CountAsync(),
                TotalCarreras = await _context.Carreras.CountAsync(),
                TotalCursos = await _context.Cursos.CountAsync(),
                MatriculasActivas = await _context.Matriculas.CountAsync(m => m.Estado == "Activa")
            };

            return View(vm);
        }
    }

    public class AdminDashboardViewModel
    {
        public int TotalEstudiantes { get; set; }
        public int TotalProfesores { get; set; }
        public int TotalCarreras { get; set; }
        public int TotalCursos { get; set; }
        public int MatriculasActivas { get; set; }
    }
}
