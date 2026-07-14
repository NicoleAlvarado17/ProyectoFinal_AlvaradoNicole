using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_AlvaradoNicole.Models;
using System.Diagnostics;

namespace ProyectoFinal_AlvaradoNicole.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Página principal
        public IActionResult Index()
        {
            return View();
        }

        // Dashboard Estudiante
        [Authorize(Roles = "Estudiante")]
        public IActionResult Dashboard()
        {
            return View();
        }

        // Dashboard Docente
        [Authorize(Roles = "Docente")]
        public IActionResult DashboardDocente()
        {
            return View();
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