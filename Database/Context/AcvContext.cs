using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

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
            CultureInfo provider = CultureInfo.InvariantCulture;
            var hasher = new PasswordHasher<AppUser>();

            var AdminId = Guid.NewGuid().ToString();
            var AdminRoleId = Guid.NewGuid().ToString();
            var UserRoleId = Guid.NewGuid().ToString();

            base.OnModelCreating(builder);

            builder.Entity<AppUser>().HasData(
                    new AppUser
                    {
                        Id = AdminId,
                        FirstName = "Admin",
                        LastName = "Admin",
                        Email = "admin@admin.com",
                        UserName = "admin@admin.com",
                        NormalizedEmail = "ADMIN@ADMIN.COM",
                        Registration = "11111111", // 8 chars
                        BirthDate = DateTime.ParseExact("05031958", "ddMMyyyy", provider),
                        Approved = true,
                        EmailConfirmed = true,
                        PasswordHash = hasher.HashPassword(null, "Psswrd@1.2?3!"),
                        SecurityStamp = string.Empty
                    }
                );

            builder.Entity<IdentityRole>().HasData(
                    new IdentityRole
                    {
                        Id = AdminRoleId,
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    },
                    new IdentityRole
                    {
                        Id = UserRoleId,
                        Name = "User",
                        NormalizedName = "USER",
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    }
                );

            builder.Entity<IdentityUserRole<string>>().HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = AdminRoleId,
                        UserId = AdminId
                    }
                );
        }
    }
}