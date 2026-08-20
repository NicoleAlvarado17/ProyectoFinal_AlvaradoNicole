using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal_AlvaradoNicole.ViewComponents
{
    public class StatCardViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string label, string value, string? subtitle = null, string? icon = null)
        {
            var vm = new StatCardViewModel
            {
                Label = label,
                Value = value,
                Subtitle = subtitle,
                Icon = icon
            };

            return View(vm);
        }
    }

    public class StatCardViewModel
    {
        public string Label { get; set; } = "";
        public string Value { get; set; } = "";
        public string? Subtitle { get; set; }
        public string? Icon { get; set; }
    }
}
