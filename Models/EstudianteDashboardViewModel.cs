namespace ProyectoFinal_AlvaradoNicole.Models
{
    public class EstudianteDashboardViewModel
    {
        public string NombreEstudiante { get; set; } = "";
        public string NombreCarrera { get; set; } = "";
        public int CreditosMatriculados { get; set; }
        public int CursosActivos { get; set; }
        public List<EstudianteDashboardCursoViewModel> Cursos { get; set; } = new();
    }

    public class EstudianteDashboardCursoViewModel
    {
        public string Nombre { get; set; } = "";
        public string Modalidad { get; set; } = "";
        public string Estado { get; set; } = "";
    }
}
