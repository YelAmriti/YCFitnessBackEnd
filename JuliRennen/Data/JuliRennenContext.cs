using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JuliRennen.Models;

namespace JuliRennen.Data
{
    public class JuliRennenContext : DbContext
    {
        public JuliRennenContext (DbContextOptions<JuliRennenContext> options)
            : base(options)
        {
        }

        public DbSet<JuliRennen.Models.User> User { get; set; } = default!;

        public DbSet<JuliRennen.Models.Route>? Route { get; set; }

        public DbSet<JuliRennen.Models.Run>? Run { get; set; }
    }
}
