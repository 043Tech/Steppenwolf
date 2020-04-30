using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Steppenwolf.Models;

namespace Steppenwolf.PostgresRepositories.Context
{
    public class PostgresDbContext : IdentityDbContext<ApplicationUser>
    {
        public PostgresDbContext(DbContextOptions options)
            : base(options)
        {
        }
        
        public DbSet<Test> Tests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().Property(p => p.Id).HasValueGenerator<GuidValueGenerator>();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}