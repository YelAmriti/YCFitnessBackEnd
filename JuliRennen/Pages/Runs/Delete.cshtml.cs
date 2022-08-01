using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JuliRennen.Data;
using JuliRennen.Models;

namespace JuliRennen.Pages.Runs
{
    public class DeleteModel : PageModel
    {
        private readonly JuliRennen.Data.JuliRennenContext _context;

        public DeleteModel(JuliRennen.Data.JuliRennenContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Run Run { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Run == null)
            {
                return NotFound();
            }

            var run = await _context.Run.FirstOrDefaultAsync(m => m.ID == id);

            if (run == null)
            {
                return NotFound();
            }
            else 
            {
                Run = run;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Run == null)
            {
                return NotFound();
            }
            var run = await _context.Run.FindAsync(id);

            if (run != null)
            {
                Run = run;
                _context.Run.Remove(Run);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
