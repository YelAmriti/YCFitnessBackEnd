using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JuliRennen.Pages
{
    public class GMapsModel : PageModel
    {
        private readonly JuliRennen.Data.JuliRennenContext _context;

        public GMapsModel(JuliRennen.Data.JuliRennenContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<SelectListItem> Options { get; set; }
        public IActionResult OnGet(int? id)
        {
            Route = _context.Route.Find(id);

            Options = _context.Route.Select(a =>
                                          new SelectListItem
                                          {
                                              Value = a.ID.ToString() + " " +
                                                    a.GPSxStart.ToString()+ " " +
                                                    a.GPSyStart.ToString() + " " +
                                                    a.GPSyEnd.ToString() + " " +
                                                    a.GPSxEnd.ToString(),
                                              Text = a.Name,
                                          }).ToList();

            return Page();
        }
        [BindProperty]
        public string RouteID { get; set; }
        public JuliRennen.Models.Route Route { get; set; } = default!;
        public void OnGetLoadRoute(int? id)
        {
            Route = _context.Route.Find(id);
        }

    }
}
