using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Context
{
    public class AcvContext : IdentityDbContext<AppUser>
    {
        public AcvContext(DbContextOptions<AcvContext> options)
            : base(options)
        {
        }

        public DbSet<Audio> Audio { get; set; }

    }
}
