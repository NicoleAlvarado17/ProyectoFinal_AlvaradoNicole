using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal_AlvaradoNicole.ViewComponents
{
    // Widget reutilizable para las tarjetas de estadísticas de los dashboards
    // (Estudiante, Docente, Administrador). Uso:
    //   @await Component.InvokeAsync("StatCard", new { label = "Cursos activos",
    //       value = "6", subtitle = "este cuatrimestre" })
    public class StatCardViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string label, string value, string? subtitle = null)
        {
            var vm = new StatCardViewModel
            {
                Label = label,
                Value = value,
                Subtitle = subtitle
            };

            return View(vm);
        }
    }

    public class StatCardViewModel
    {
        public string Label { get; set; } = "";
        public string Value { get; set; } = "";
        public string? Subtitle { get; set; }
    }
}
