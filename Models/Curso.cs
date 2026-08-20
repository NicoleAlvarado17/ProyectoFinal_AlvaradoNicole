using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SistemaMatriculaURA.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int Creditos { get; set; }
        public decimal Costo { get; set; }
        public string Modalidad { get; set; } = "";
        public string Sede { get; set; } = "";
        public string Horario { get; set; } = "";
        public string Grupo { get; set; } = "";
        public string Aula { get; set; } = "";
        public string Estado { get; set; } = "Activo";


        public int? DocenteId { get; set; }
        [ValidateNever]
        public Docente Docente { get; set; }

        public int CarreraId { get; set; }

        [ValidateNever]
        public Carrera Carrera { get; set; }

        [ValidateNever]
        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
