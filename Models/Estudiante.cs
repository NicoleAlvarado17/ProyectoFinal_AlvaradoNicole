namespace SistemaMatriculaURA.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }
        public System.Collections.Generic.ICollection<Matricula> Matriculas { get; set; }
    }
}
