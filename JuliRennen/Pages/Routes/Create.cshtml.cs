using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JuliRennen.Data;
using JuliRennen.Models;

namespace JuliRennen.Pages.Routes
{
    public class CreateModel : PageModel
    {
        private readonly JuliRennen.Data.JuliRennenContext _context;

        public CreateModel(JuliRennen.Data.JuliRennenContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<SelectListItem> Options { get; set; }
        public IActionResult OnGet()
        {
            Options = _context.User.Select(a =>
                                          new SelectListItem
                                          {
                                              Value = a.ID.ToString(),
                                              Text = a.UserName
                                          }).ToList();

            return Page();
        }

        [BindProperty]
        public JuliRennen.Models.Route Route { get; set; } = default!;
        [BindProperty]
        public string UserID { get; set; }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Route == null || Route == null)
            {
                return Page();
            }

            User s = _context.User.Find(Int32.Parse(UserID));
            Route.User = s;

            _context.Route.Add(Route);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
