using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JuliRennen.Data;
using JuliRennen.Models;

namespace JuliRennen.Pages.Account
{
    public class UserIndexModel : PageModel
    {
        private readonly JuliRennen.Data.JuliRennenContext _context;

        public UserIndexModel(JuliRennen.Data.JuliRennenContext context)
        {
            _context = context;
        }

        public User User { get; set; } = default!;
        public IList<JuliRennen.Models.Route> Route { get; set; } = default!;
        public IList<JuliRennen.Models.Run> Run { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            var id = HttpContext.Session.GetInt32("UserID");

            if (id == null || _context.User == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.ID == id);

            if (_context.Route != null)
            {
                Route = await _context.Route.Where(route => route.User.ID == id).ToListAsync();
            }
            if (_context.Run != null)
            {
                Run = await _context.Run.Where(run => run.User.ID == id).ToListAsync();
            }

            if (user == null)
            {
                return NotFound();
            }
            else 
            {
                User = user;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostLogOut()
        {
            Response.Cookies.Delete(".AspNetCore.Session");
            Response.Cookies.Delete("MyCookieAuth");

            return RedirectToPage("/Account/Login");
        }
    }
}
