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
    public class RouteIndexModel : PageModel
    {
        private readonly JuliRennen.Data.JuliRennenContext _context;

        public RouteIndexModel(JuliRennen.Data.JuliRennenContext context)
        {
            _context = context;
        }

        public IList<JuliRennen.Models.Route> Route { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var id = HttpContext.Session.GetInt32("UserID");

            if (_context.Route != null)
            {
                Route = await _context.Route.Where(route => route.User.ID == id).ToListAsync();
            }
        }
    }
}
