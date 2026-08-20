using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal_AlvaradoNicole.ViewComponents
{
    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int pageIndex, int totalPages, string action, object? routeValues = null)
        {
            var vm = new PaginationViewModel
            {
                PageIndex = pageIndex,
                TotalPages = totalPages,
                Action = action,
                RouteValues = routeValues
            };

            return View(vm);
        }
    }

    public class PaginationViewModel
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public string Action { get; set; } = "Index";
        public object? RouteValues { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
