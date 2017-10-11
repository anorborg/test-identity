using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public class ApplicationIdentityDbContext :
        IdentityDbContext<ApplicationUser>
    {
        public ApplicationIdentityDbContext()
            : base()
        {

        }
        public ApplicationIdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ApplicationIdentity.db");

            optionsBuilder.UseOpenIddict();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.HasDefaultSchema("App");

            builder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("IdentityUsers");
                b.Property(p => p.Id).HasColumnName("UserId");
            });

            /*builder.Entity<ApplicationUserClaim>(b =>
            {
                b.ToTable("IdentityUserClaims");
                b.Property(p => p.Id).HasColumnName("UserClaimId");
            });

            builder.Entity<ApplicationUserLogin>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
                b.ToTable("IdentityLogins");
            });

            builder.Entity<ApplicationUserToken>(b => 
            {
                b.ToTable("IdentityTokens");
            });*/

            builder.Entity<ApplicationRole>(b =>
            {
                b.ToTable("IdentityRoles");
                b.Property(p => p.Id).HasColumnName("RoleId");
            });

            /*builder.Entity<ApplicationUserClaim>(b =>
            {
                b.ToTable("IdentityUserClaims");
                b.Property(p => p.Id).HasColumnName("UserClaimId");
            });

            builder.Entity<ApplicationUserRole>(b =>
            {
                b.ToTable("IdentityUserRoles");
            });*/
        }
    }
}
