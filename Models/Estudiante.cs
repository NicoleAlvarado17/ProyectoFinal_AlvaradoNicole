using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SistemaMatriculaURA.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public int CarreraId { get; set; }

        // [ValidateNever]: esta propiedad de navegación no viene en el formulario
        // (solo se envía CarreraId). Sin este atributo, ASP.NET Core la marca como
        // "requerida" por ser un tipo de referencia no-nullable, el ModelState queda
        // inválido y el Create/Edit "no guarda" sin mostrar ningún error visible.
        [ValidateNever]
        public Carrera Carrera { get; set; }

        public string? UserId { get; set; }

        [ValidateNever]
        public System.Collections.Generic.ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}