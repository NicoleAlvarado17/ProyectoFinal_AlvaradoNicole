namespace ProyectoFinal_AlvaradoNicole.Models
{
    public class EstudianteDashboardViewModel
    {
        public string NombreEstudiante { get; set; } = "";
        public string NombreCarrera { get; set; } = "";
        public int CreditosMatriculados { get; set; }
        public int CursosActivos { get; set; }
        public List<EstudianteDashboardCursoViewModel> Cursos { get; set; } = new();
        public double? PromedioGeneral { get; set; }
        public int CreditosCarreraCompletados { get; set; }
        public int CreditosCarreraTotales { get; set; }
        public double ProgresoCarreraPorcentaje { get; set; }
        public List<EstudianteDashboardProgresoCursoViewModel> ProgresoCursos { get; set; } = new();

        public string DesempenoTexto =>
            PromedioGeneral == null ? "" :
            PromedioGeneral >= 90 ? "Desempeño excelente" :
            PromedioGeneral >= 80 ? "Desempeño alto" :
            PromedioGeneral >= 70 ? "Desempeño satisfactorio" :
            "Desempeño bajo";
    }

    public class EstudianteDashboardCursoViewModel
    {
        public string Nombre { get; set; } = "";
        public string Modalidad { get; set; } = "";
        public string Estado { get; set; } = "";
    }
    public class EstudianteDashboardProgresoCursoViewModel
    {
        public string Nombre { get; set; } = "";
        public double? PorcentajeAsistencia { get; set; }
    }
}
