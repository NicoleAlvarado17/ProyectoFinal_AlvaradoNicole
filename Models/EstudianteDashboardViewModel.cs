namespace ProyectoFinal_AlvaradoNicole.Models
{
    public class EstudianteDashboardViewModel
    {
        public string NombreEstudiante { get; set; } = "";
        public string NombreCarrera { get; set; } = "";
        public int CreditosMatriculados { get; set; }
        public int CursosActivos { get; set; }
        public List<EstudianteDashboardCursoViewModel> Cursos { get; set; } = new();

        // HU07/HU10 - Promedio ponderado (por créditos) de los cursos ya finalizados
        // (Aprobada/Reprobada, con Nota asignada). Null si aún no tiene cursos finalizados.
        public double? PromedioGeneral { get; set; }

        // HU10 - Progreso de la carrera: créditos aprobados / créditos totales del
        // plan de estudios de la carrera del estudiante.
        public int CreditosCarreraCompletados { get; set; }
        public int CreditosCarreraTotales { get; set; }
        public double ProgresoCarreraPorcentaje { get; set; }

        // HU10 - Progreso del cuatrimestre actual: % de asistencia por curso activo.
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

    // HU10 - Progreso (% de asistencia) de un curso activo del cuatrimestre actual.
    public class EstudianteDashboardProgresoCursoViewModel
    {
        public string Nombre { get; set; } = "";
        public double? PorcentajeAsistencia { get; set; }
    }
}
