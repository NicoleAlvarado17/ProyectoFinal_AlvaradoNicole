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
        public string Cuatrimestre { get; set; } = "";
        public int? Nota { get; set; }
    }
}
