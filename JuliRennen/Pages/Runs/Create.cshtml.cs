using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JuliRennen.Data;
using JuliRennen.Models;

namespace JuliRennen.Pages.Runs
{
    public class CreateModel : PageModel
    {
        private readonly JuliRennen.Data.JuliRennenContext _context;

        public CreateModel(JuliRennen.Data.JuliRennenContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<SelectListItem> OptionsU { get; set; }
        public List<SelectListItem> OptionsR { get; set; }
        public IActionResult OnGet()
        {
            OptionsU = _context.User.Select(a =>
                                          new SelectListItem
                                          {
                                              Value = a.ID.ToString(),
                                              Text = a.UserName
                                          }).ToList();

            OptionsR = _context.Route.Select(a =>
                                          new SelectListItem
                                          {
                                              Value = a.ID.ToString(),
                                              Text = a.Name
                                          }).ToList();

            return Page();
        }

        [BindProperty]
        public Run Run { get; set; } = default!;
        [BindProperty]
        public string UserID { get; set; }
        [BindProperty]
        public string RouteID { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Run == null || Run == null)
            {
                return Page();
            }

            User s = _context.User.Find(Int32.Parse(UserID));
            Run.User = s;
            JuliRennen.Models.Route t = _context.Route.Find(Int32.Parse(RouteID));
            Run.Route = t;

            _context.Run.Add(Run);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
