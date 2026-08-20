using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SistemaMatriculaURA.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public int CarreraId { get; set; }
        [ValidateNever]
        public Carrera Carrera { get; set; }

        public string? UserId { get; set; }

        [ValidateNever]
        public System.Collections.Generic.ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}