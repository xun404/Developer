using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{
    public class APIDbContext : IdentityDbContext<APIUser>
    {
        public APIDbContext(DbContextOptions<APIDbContext> options)
            : base(options)
        {
        }

        public DbSet<App> Apps { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
