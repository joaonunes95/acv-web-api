using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database.Context
{
    public class AcvContext : IdentityDbContext<AppUser>
    {
        public AcvContext(DbContextOptions<AcvContext> options)
            : base(options)
        {
        }

        public DbSet<Audio> Audio { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Channel> Channel { get; set; }
        public DbSet<Speaker> Speaker { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
