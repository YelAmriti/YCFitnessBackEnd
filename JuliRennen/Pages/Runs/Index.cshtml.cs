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
    public class IndexModel : PageModel
    {
        private readonly JuliRennen.Data.JuliRennenContext _context;

        public IndexModel(JuliRennen.Data.JuliRennenContext context)
        {
            _context = context;
        }

        public IList<Run> Run { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Run != null)
            {
                Run = await _context.Run.ToListAsync();
            }
        }
    }
}
