﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JuliRennen.Data;
using JuliRennen.Models;
using Microsoft.AspNetCore.Authorization;

namespace JuliRennen.Pages.Users
{
    [Authorize(Policy="AdminOnly")]
    public class IndexModel : PageModel
    {
        private readonly JuliRennen.Data.JuliRennenContext _context;

        public IndexModel(JuliRennen.Data.JuliRennenContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.User != null)
            {
                User = await _context.User.ToListAsync();
            }
        }
    }
}
