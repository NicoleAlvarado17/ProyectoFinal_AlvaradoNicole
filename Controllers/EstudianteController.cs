using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMatriculaURA.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Globalization;

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

        private const int PageSizeCursos = 6;

        // HU05/HU09 - catálogo de cursos disponibles, con paginación y filtros
        // (créditos, modalidad, texto libre) resueltos vía AJAX en CursosParcial.
        public async Task<IActionResult> Cursos(int page = 1, int? creditos = null, string? modalidad = null, string? busqueda = null)
        {
            var estudiante = await GetOrCreateEstudianteAsync();
            if (estudiante == null)
            {
                TempData["Error"] = "No hay carreras registradas en el sistema todavia.";
                return RedirectToAction("Dashboard", "Home");
            }

            ViewBag.Modalidades = ProyectoFinal_AlvaradoNicole.Controllers.CursosAdminController.Modalidades;
            ViewBag.CreditosSeleccionado = creditos;
            ViewBag.ModalidadSeleccionada = modalidad;
            ViewBag.Busqueda = busqueda;

            var vm = await ObtenerCursosDisponiblesAsync(estudiante, page, creditos, modalidad, busqueda);
            return View(vm);
        }

        // Devuelve solo el fragmento de tabla + paginación, para refrescar el
        // catálogo mediante AJAX sin recargar la página completa.
        [HttpGet]
        public async Task<IActionResult> CursosParcial(int page = 1, int? creditos = null, string? modalidad = null, string? busqueda = null)
        {
            ViewBag.CreditosSeleccionado = creditos;
            ViewBag.ModalidadSeleccionada = modalidad;
            ViewBag.Busqueda = busqueda;

            var estudiante = await GetOrCreateEstudianteAsync();
            if (estudiante == null)
            {
                return PartialView("_CursosDisponiblesParcial", new PaginatedList<CursoDisponibleViewModel>(new List<CursoDisponibleViewModel>(), 0, 1, PageSizeCursos));
            }

            var vm = await ObtenerCursosDisponiblesAsync(estudiante, page, creditos, modalidad, busqueda);
            return PartialView("_CursosDisponiblesParcial", vm);
        }

        private async Task<PaginatedList<CursoDisponibleViewModel>> ObtenerCursosDisponiblesAsync(
            Estudiante estudiante, int page, int? creditos, string? modalidad, string? busqueda)
        {
            var misMatriculas = await _context.Matriculas
                .Where(m => m.EstudianteId == estudiante.Id)
                .Select(m => m.CursoId)
                .ToListAsync();

            var query = _context.Cursos
                .Include(c => c.Carrera)
                .Where(c => c.Estado == "Activo")
                .AsQueryable();

            if (creditos.HasValue)
                query = query.Where(c => c.Creditos == creditos.Value);

            if (!string.IsNullOrWhiteSpace(modalidad))
                query = query.Where(c => c.Modalidad == modalidad);

            if (!string.IsNullOrWhiteSpace(busqueda))
                query = query.Where(c => c.Nombre.Contains(busqueda) || c.Codigo.Contains(busqueda));

            query = query.OrderBy(c => c.Codigo);

            var paginaCursos = await PaginatedList<Curso>.CreateAsync(query, page, PageSizeCursos);

            var items = paginaCursos.Select(c => new CursoDisponibleViewModel
            {
                Id = c.Id,
                Codigo = c.Codigo,
                Nombre = c.Nombre,
                Creditos = c.Creditos,
                Costo = c.Costo,
                Modalidad = c.Modalidad,
                Sede = c.Sede,
                CarreraNombre = c.Carrera != null ? c.Carrera.Nombre : "",
                YaMatriculado = misMatriculas.Contains(c.Id)
            }).ToList();

            return new PaginatedList<CursoDisponibleViewModel>(items, paginaCursos.TotalCount, paginaCursos.PageIndex, PageSizeCursos);
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
                var curso = await _context.Cursos.FindAsync(cursoId);
                if (curso == null)
                {
                    TempData["Error"] = "El curso seleccionado ya no existe.";
                    return RedirectToAction(nameof(Cursos));
                }

                var matricula = new Matricula
                {
                    EstudianteId = estudiante.Id,
                    CursoId = cursoId,
                    Fecha = DateTime.Now,
                    Estado = "Activa"
                };
                _context.Matriculas.Add(matricula);
                await _context.SaveChangesAsync();

                // Registra la transacción de pago de la matrícula (HU-Pago). El número
                // de transacción se basa en el Id de la matrícula para que sea único
                // y fácil de rastrear en el comprobante.
                var pago = new Pago
                {
                    MatriculaId = matricula.Id,
                    Monto = curso.Costo,
                    FechaPago = DateTime.Now,
                    NumeroTransaccion = $"TXN-{matricula.Id:D8}",
                    MetodoPago = "Tarjeta de crédito",
                    Estado = "Completado"
                };
                _context.Pagos.Add(pago);
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
                Costo = m.Curso.Costo,
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

        // HU11 - Historial académico: cursos ya finalizados (con nota asignada),
        // de cuatrimestres anteriores. "Aprobada" si Nota >= 70, "Reprobada" si no.
        public async Task<IActionResult> Historial()
        {
            var estudiante = await GetOrCreateEstudianteAsync();
            if (estudiante == null)
            {
                TempData["Error"] = "No se pudo identificar tu perfil de estudiante.";
                return RedirectToAction("Dashboard", "Home");
            }

            var historial = await _context.Matriculas
                .Include(m => m.Curso)
                .Where(m => m.EstudianteId == estudiante.Id && m.Nota != null)
                .OrderByDescending(m => m.Cuatrimestre)
                .ThenBy(m => m.Curso.Nombre)
                .ToListAsync();

            var vm = historial.Select(m => new HistorialAcademicoItemViewModel
            {
                Curso = m.Curso.Nombre,
                Codigo = m.Curso.Codigo,
                Cuatrimestre = m.Cuatrimestre,
                Nota = m.Nota ?? 0,
                Estado = m.Estado
            }).ToList();

            return View(vm);
        }

        // Comprobante de matrícula en formato de factura: incluye número de
        // comprobante, datos del estudiante, tabla de cursos con su costo, total
        // y el detalle de las transacciones de pago (tabla Pagos) asociadas.
        public async Task<IActionResult> Comprobante()
        {
            var estudiante = await GetOrCreateEstudianteAsync();
            if (estudiante == null)
            {
                TempData["Error"] = "No se pudo generar el comprobante: no se encontró tu perfil de estudiante.";
                return RedirectToAction(nameof(MisCursos));
            }

            var matriculas = await _context.Matriculas
                .Include(m => m.Curso)
                .Where(m => m.EstudianteId == estudiante.Id)
                .OrderBy(m => m.Fecha)
                .ToListAsync();

            var matriculaIds = matriculas.Select(m => m.Id).ToList();
            var pagos = await _context.Pagos
                .Where(p => matriculaIds.Contains(p.MatriculaId))
                .OrderBy(p => p.FechaPago)
                .ToListAsync();

            var carrera = await _context.Carreras.FirstOrDefaultAsync(c => c.Id == estudiante.CarreraId);

            using var ms = new MemoryStream();
            var doc = new Document(PageSize.Letter, 45, 45, 50, 50);
            PdfWriter.GetInstance(doc, ms);
            doc.Open();

            var azulURA = new BaseColor(20, 40, 90);
            var fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, azulURA);
            var fontSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.DarkGray);
            var fontPequena = FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.Gray);
            var fontEtiqueta = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
            var fontTexto = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fontHeaderTabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, BaseColor.White);
            var fontCeldaTabla = FontFactory.GetFont(FontFactory.HELVETICA, 9);
            var fontTotal = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13, azulURA);

            var numeroComprobante = $"CMP-{estudiante.Id:D6}-{DateTime.Now:yyyyMMddHHmmss}";

            doc.Add(new Paragraph("Universidad Real Americana", fontTitulo) { Alignment = Element.ALIGN_CENTER });
            doc.Add(new Paragraph("Comprobante de Matrícula", fontSubtitulo) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 2 });
            doc.Add(new Paragraph("San José, Costa Rica  ·  Tel. +506 2222-3344  ·  info@universidadrealamericana.edu", fontPequena) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 10 });
            doc.Add(new Paragraph(new string('-', 100), fontPequena) { SpacingAfter = 10 });

            doc.Add(new Paragraph($"N° de comprobante: {numeroComprobante}", fontEtiqueta));
            doc.Add(new Paragraph($"Fecha de emisión: {DateTime.Now:dd/MM/yyyy HH:mm}", fontTexto) { SpacingAfter = 8 });

            doc.Add(new Paragraph($"Estudiante: {estudiante.Nombre}", fontEtiqueta));
            doc.Add(new Paragraph($"Correo: {estudiante.Correo}", fontTexto));
            doc.Add(new Paragraph($"Carrera: {carrera?.Nombre ?? "Sin carrera asignada"}", fontTexto));
            doc.Add(new Paragraph($"Identificación de estudiante: EST-{estudiante.Id:D5}", fontTexto) { SpacingAfter = 12 });

            if (!matriculas.Any())
            {
                doc.Add(new Paragraph("No tienes cursos matriculados actualmente.", fontTexto));
            }
            else
            {
                var tabla = new PdfPTable(5) { WidthPercentage = 100, SpacingBefore = 6 };
                tabla.SetWidths(new float[] { 1.2f, 3f, 1f, 1.4f, 1.4f });

                foreach (var encabezado in new[] { "Código", "Curso", "Créd.", "Estado", "Costo" })
                {
                    var celda = new PdfPCell(new Phrase(encabezado, fontHeaderTabla))
                    {
                        BackgroundColor = azulURA,
                        Padding = 6,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    tabla.AddCell(celda);
                }

                decimal total = 0m;
                foreach (var m in matriculas)
                {
                    tabla.AddCell(new PdfPCell(new Phrase(m.Curso.Codigo, fontCeldaTabla)) { Padding = 5, HorizontalAlignment = Element.ALIGN_CENTER });
                    tabla.AddCell(new PdfPCell(new Phrase(m.Curso.Nombre, fontCeldaTabla)) { Padding = 5 });
                    tabla.AddCell(new PdfPCell(new Phrase(m.Curso.Creditos.ToString(), fontCeldaTabla)) { Padding = 5, HorizontalAlignment = Element.ALIGN_CENTER });
                    tabla.AddCell(new PdfPCell(new Phrase(m.Estado, fontCeldaTabla)) { Padding = 5, HorizontalAlignment = Element.ALIGN_CENTER });
                    tabla.AddCell(new PdfPCell(new Phrase("₡" + m.Curso.Costo.ToString("N0", CultureInfo.InvariantCulture), fontCeldaTabla)) { Padding = 5, HorizontalAlignment = Element.ALIGN_RIGHT });
                    total += m.Curso.Costo;
                }

                var celdaTotalEtiqueta = new PdfPCell(new Phrase("TOTAL", fontTotal))
                {
                    Colspan = 4,
                    Border = Rectangle.TOP_BORDER,
                    BorderColor = azulURA,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    Padding = 8
                };
                var celdaTotalValor = new PdfPCell(new Phrase("₡" + total.ToString("N0", CultureInfo.InvariantCulture), fontTotal))
                {
                    Border = Rectangle.TOP_BORDER,
                    BorderColor = azulURA,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    Padding = 8
                };
                tabla.AddCell(celdaTotalEtiqueta);
                tabla.AddCell(celdaTotalValor);

                doc.Add(tabla);

                if (pagos.Any())
                {
                    doc.Add(new Paragraph("Detalle de pago", fontEtiqueta) { SpacingBefore = 14, SpacingAfter = 4 });
                    foreach (var p in pagos)
                    {
                        doc.Add(new Paragraph(
                            $"Transacción {p.NumeroTransaccion}  ·  {p.MetodoPago}  ·  {p.FechaPago:dd/MM/yyyy}  ·  {p.Estado}  ·  ₡{p.Monto.ToString("N0", CultureInfo.InvariantCulture)}",
                            fontPequena));
                    }
                }
            }

            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(
                "Este comprobante fue generado automáticamente por el Sistema de Matrícula Académica de la Universidad Real Americana y es válido como constancia de matrícula y pago.",
                fontPequena) { Alignment = Element.ALIGN_CENTER, SpacingBefore = 20 });

            doc.Close();

            var nombreArchivo = "ComprobanteMatricula_" + estudiante.Nombre.Replace(" ", "_") + ".pdf";
            return File(ms.ToArray(), "application/pdf", nombreArchivo);
        }
    }

    public class CursoDisponibleViewModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int Creditos { get; set; }
        public decimal Costo { get; set; }
        public string Modalidad { get; set; } = "";
        public string Sede { get; set; } = "";
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
        public decimal Costo { get; set; }
        public string Estado { get; set; } = "";
        public DateTime Fecha { get; set; }

        public string Modalidad { get; set; } = "";
        public string Sede { get; set; } = "";
        public string Horario { get; set; } = "";
        public string Docente { get; set; } = "";
    }

    // HU11 - Fila del historial académico del estudiante.
    public class HistorialAcademicoItemViewModel
    {
        public string Curso { get; set; } = "";
        public string Codigo { get; set; } = "";
        public string Cuatrimestre { get; set; } = "";
        public int Nota { get; set; }
        public string Estado { get; set; } = "";
    }
}
