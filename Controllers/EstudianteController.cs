using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMatriculaURA.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace ProyectoFinal_AlvaradoNicole.Controllers
{
    [Authorize(Roles = "Estudiante")]
    public class EstudianteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EstudianteController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<Estudiante?> GetOrCreateEstudianteAsync()
        {
            var userId = _userManager.GetUserId(User);
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(e => e.UserId == userId);

            if (estudiante == null)
            {
                var email = User.Identity?.Name;
                estudiante = await _context.Estudiantes.FirstOrDefaultAsync(e => e.Correo == email);

                if (estudiante != null)
                {
                    estudiante.UserId = userId;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var carrera = await _context.Carreras.FirstOrDefaultAsync();
                    if (carrera == null) return null;

                    estudiante = new Estudiante
                    {
                        Nombre = User.Identity?.Name ?? "Estudiante",
                        Correo = email ?? "",
                        CarreraId = carrera.Id,
                        UserId = userId
                    };
                    _context.Estudiantes.Add(estudiante);
                    await _context.SaveChangesAsync();
                }
            }

            return estudiante;
        }

        public async Task<IActionResult> Cursos()
        {
            var estudiante = await GetOrCreateEstudianteAsync();
            if (estudiante == null)
            {
                TempData["Error"] = "No hay carreras registradas en el sistema todavia.";
                return RedirectToAction("Dashboard", "Home");
            }

            var cursos = await _context.Cursos.Include(c => c.Carrera).ToListAsync();
            var misMatriculas = await _context.Matriculas
                .Where(m => m.EstudianteId == estudiante.Id)
                .Select(m => m.CursoId)
                .ToListAsync();

            var vm = cursos.Select(c => new CursoDisponibleViewModel
            {
                Id = c.Id,
                Codigo = c.Codigo,
                Nombre = c.Nombre,
                Creditos = c.Creditos,
                CarreraNombre = c.Carrera != null ? c.Carrera.Nombre : "",
                YaMatriculado = misMatriculas.Contains(c.Id)
            }).ToList();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Matricular(int cursoId)
        {
            var estudiante = await GetOrCreateEstudianteAsync();
            if (estudiante == null)
            {
                TempData["Error"] = "No se pudo identificar tu perfil de estudiante.";
                return RedirectToAction(nameof(Cursos));
            }

            var yaExiste = await _context.Matriculas
                .AnyAsync(m => m.EstudianteId == estudiante.Id && m.CursoId == cursoId);

            if (!yaExiste)
            {
                var matricula = new Matricula
                {
                    EstudianteId = estudiante.Id,
                    CursoId = cursoId,
                    Fecha = DateTime.Now,
                    Estado = "Activa"
                };
                _context.Matriculas.Add(matricula);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Curso matriculado correctamente.";
            }

            return RedirectToAction(nameof(Cursos));
        }

        public async Task<IActionResult> MisCursos()
        {
            var estudiante = await GetOrCreateEstudianteAsync();
            if (estudiante == null)
            {
                TempData["Error"] = "No se pudo identificar tu perfil de estudiante.";
                return RedirectToAction("Dashboard", "Home");
            }

            var matriculas = await _context.Matriculas
                .Include(m => m.Curso)
                .ThenInclude(c => c.Carrera)
                .Where(m => m.EstudianteId == estudiante.Id && m.Estado == "Activa")
                .ToListAsync();

            var vm = matriculas.Select(m => new MiCursoViewModel
            {
                MatriculaId = m.Id,
                CursoId = m.CursoId,
                Codigo = m.Curso.Codigo,
                Nombre = m.Curso.Nombre,
                Carrera = m.Curso.Carrera != null ? m.Curso.Carrera.Nombre : "",
                Creditos = m.Curso.Creditos,
                Estado = m.Estado,
                Fecha = m.Fecha,
                Modalidad = m.Curso.Modalidad,
                Sede = m.Curso.Sede,
                Horario = m.Curso.Horario,
                Docente = m.Curso.Docente?.Nombre ?? "Sin asignar"

            }).ToList();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Congelar(int matriculaId)
        {
            var matricula = await _context.Matriculas.FirstOrDefaultAsync(m => m.Id == matriculaId);

            if (matricula != null)
            {
                matricula.Estado = "Congelado";
                await _context.SaveChangesAsync();
                TempData["Success"] = "Curso congelado correctamente.";
            }

            return RedirectToAction(nameof(MisCursos));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Desmatricular(int cursoId)
        {
            var estudiante = await GetOrCreateEstudianteAsync();
            if (estudiante == null)
            {
                return RedirectToAction(nameof(MisCursos));
            }

            var matricula = await _context.Matriculas
                .FirstOrDefaultAsync(m => m.EstudianteId == estudiante.Id && m.CursoId == cursoId);

            if (matricula != null)
            {
                _context.Matriculas.Remove(matricula);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Curso eliminado de tu matrícula.";
            }

            return RedirectToAction(nameof(MisCursos));
        }

        public async Task<IActionResult> Comprobante()
        {
            var estudiante = await GetOrCreateEstudianteAsync();
            var matriculas = await _context.Matriculas
                .Include(m => m.Curso)
                .Where(m => m.EstudianteId == estudiante.Id)
                .ToListAsync();

            using var ms = new MemoryStream();
            var doc = new Document();
            PdfWriter.GetInstance(doc, ms);

            doc.Open();
            doc.Add(new Paragraph("Comprobante de Matrícula"));
            doc.Add(new Paragraph($"Estudiante: {estudiante.Nombre}"));
            doc.Add(new Paragraph($"Correo: {estudiante.Correo}"));
            doc.Add(new Paragraph(" "));

            foreach (var m in matriculas)
            {
                doc.Add(new Paragraph($"{m.Curso.Codigo} - {m.Curso.Nombre} ({m.Estado})"));
            }

            doc.Close();

            return File(ms.ToArray(), "application/pdf", "ComprobanteMatricula.pdf");
        }
    }

    public class CursoDisponibleViewModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int Creditos { get; set; }
        public string CarreraNombre { get; set; } = "";
        public bool YaMatriculado { get; set; }
    }

  

    public class MiCursoViewModel
    {
        public int MatriculaId { get; set; }
        public int CursoId { get; set; }
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Carrera { get; set; } = "";
        public int Creditos { get; set; }
        public string Estado { get; set; } = "";
        public DateTime Fecha { get; set; }

        public string Modalidad { get; set; } = "";
        public string Sede { get; set; } = "";
        public string Horario { get; set; } = "";
        public string Docente { get; set; } = "";
    }
}
