using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JuliRennen.Models;
using Microsoft.EntityFrameworkCore;

namespace JuliRennen.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public User User { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {

            return Page();
        }
    }
}