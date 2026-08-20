using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SistemaMatriculaURA.Models
{
    public class Asistencia
    {
        public int Id { get; set; }

        public int CursoId { get; set; }

        [ValidateNever]
        public Curso Curso { get; set; }

        public int EstudianteId { get; set; }

        [ValidateNever]
        public Estudiante Estudiante { get; set; }
        public DateTime Fecha { get; set; }

        public bool Presente { get; set; }
    }
}
