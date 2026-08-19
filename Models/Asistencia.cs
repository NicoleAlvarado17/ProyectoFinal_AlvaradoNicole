using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SistemaMatriculaURA.Models
{
    // HU14 - Control de Asistencia. Un registro por estudiante/curso/fecha de sesión.
    public class Asistencia
    {
        public int Id { get; set; }

        public int CursoId { get; set; }

        [ValidateNever]
        public Curso Curso { get; set; }

        public int EstudianteId { get; set; }

        [ValidateNever]
        public Estudiante Estudiante { get; set; }

        // Fecha de la sesión (solo la parte de fecha, sin hora) para poder llevar
        // un historial de asistencia por día en vez de sobrescribir siempre el mismo registro.
        public DateTime Fecha { get; set; }

        public bool Presente { get; set; }
    }
}
