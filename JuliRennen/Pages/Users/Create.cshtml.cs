using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JuliRennen.Data;
using JuliRennen.Models;

namespace JuliRennen.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly JuliRennen.Data.JuliRennenContext _context;
        private IWebHostEnvironment _environment;
                
        public CreateModel(JuliRennen.Data.JuliRennenContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty] 
        public User User { get; set; } = default!;
        [BindProperty]
        public IFormFile Upload { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string file = Path.Combine(_environment.ContentRootPath, "wwwroot", "images", Upload.FileName);

            if (!ModelState.IsValid || _context.User == null || User == null)
            {
                return Page();
            }
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }

            User.ProfilePic = Path.Combine("images", Upload.FileName);
            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
