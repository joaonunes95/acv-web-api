using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Context
{
    public class AcvContext : DbContext
    {
        public AcvContext(DbContextOptions<AcvContext> options)
            : base(options)
        {
        }

        public DbSet<Audio> Audio { get; set; }

    }
}
