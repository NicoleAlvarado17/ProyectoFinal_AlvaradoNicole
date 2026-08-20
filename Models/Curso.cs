using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SistemaMatriculaURA.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int Creditos { get; set; }

        // Costo del curso en colones (₡), usado en el catálogo y en el
        // comprobante de matrícula (factura) que descarga el estudiante.
        public decimal Costo { get; set; }

        // Campos adicionales usados por las vistas
        public string Modalidad { get; set; } = "";
        public string Sede { get; set; } = "";
        public string Horario { get; set; } = "";

        // Grupo y aula de la sesión (ej. "A", "Aula 204"), usados en la sección
        // "Horarios del día" del Panel Docente.
        public string Grupo { get; set; } = "";
        public string Aula { get; set; } = "";

        // HU19 - Estado del curso: Activo, Inactivo o Cerrado.
        public string Estado { get; set; } = "Activo";


        public int? DocenteId { get; set; }

        // [ValidateNever]: propiedades de navegación que no vienen en el formulario
        // (solo se envían los Id). Sin esto, ASP.NET Core las exige por ser tipos de
        // referencia no-nullable y el ModelState queda inválido al crear/editar.
        [ValidateNever]
        public Docente Docente { get; set; }

        public int CarreraId { get; set; }

        [ValidateNever]
        public Carrera Carrera { get; set; }

        [ValidateNever]
        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
