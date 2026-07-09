namespace SistemaMatriculaURA.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int Creditos { get; set; }
        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }
        public System.Collections.Generic.ICollection<Matricula> Matriculas { get; set; }
    }
}
