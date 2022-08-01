using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JuliRennen.Data;
using JuliRennen.Models;

namespace JuliRennen.Pages.Routes
{
    public class DeleteModel : PageModel
    {
        private readonly JuliRennen.Data.JuliRennenContext _context;

        public DeleteModel(JuliRennen.Data.JuliRennenContext context)
        {
            _context = context;
        }

        [BindProperty]
      public JuliRennen.Models.Route Route { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Route == null)
            {
                return NotFound();
            }

            var route = await _context.Route.FirstOrDefaultAsync(m => m.ID == id);

            if (route == null)
            {
                return NotFound();
            }
            else 
            {
                Route = route;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Route == null)
            {
                return NotFound();
            }
            var route = await _context.Route.FindAsync(id);

            if (route != null)
            {
                Route = route;
                _context.Route.Remove(Route);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
