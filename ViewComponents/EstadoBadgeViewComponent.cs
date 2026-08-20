using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal_AlvaradoNicole.ViewComponents
{
    public class EstadoBadgeViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string estado)
        {
            return View(model: estado);
        }
    }
}
