using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal_AlvaradoNicole.ViewComponents
{
    // Widget reutilizable que colorea un estado (Activo/Inactivo/Cerrado,
    // Activa/Congelado, etc.) de forma consistente en toda la aplicación.
    // Uso: @await Component.InvokeAsync("EstadoBadge", new { estado = c.Estado })
    public class EstadoBadgeViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string estado)
        {
            return View(model: estado);
        }
    }
}
