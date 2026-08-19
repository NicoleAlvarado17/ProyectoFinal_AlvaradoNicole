
namespace SistemaMatriculaURA.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int Creditos { get; set; }

        // Campos adicionales usados por las vistas
        public string Modalidad { get; set; } = "";  
        public string Sede { get; set; } = "";
        public string Horario { get; set; } = "";

       
        public int? DocenteId { get; set; }
        public Docente Docente { get; set; }

        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }

        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
