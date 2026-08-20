namespace SistemaMatriculaURA.Models
{
    public class Matricula
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }

        // HU11 - Historial académico: cuatrimestre en el que se cursó (ej. "I-2025")
        // y nota final (0-100). Nota queda null mientras la matrícula está "Activa";
        // se llena cuando el curso finaliza (Estado pasa a "Aprobada" o "Reprobada").
        public string Cuatrimestre { get; set; } = "";
        public int? Nota { get; set; }
    }
}
