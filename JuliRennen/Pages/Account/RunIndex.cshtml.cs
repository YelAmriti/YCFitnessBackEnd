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
    public class RunIndexModel : PageModel
    {
        private readonly JuliRennen.Data.JuliRennenContext _context;

        public RunIndexModel(JuliRennen.Data.JuliRennenContext context)
        {
            _context = context;
        }

        public IList<Run> Run { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var id = HttpContext.Session.GetInt32("UserID");

            if (_context.Run != null)
            {
                Run = await _context.Run.Where(run => run.User.ID == id).ToListAsync();
            }
        }
    }
}
