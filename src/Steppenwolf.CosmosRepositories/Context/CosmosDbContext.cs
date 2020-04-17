using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Steppenwolf.Models;

namespace Steppenwolf.CosmosRepositories.Context
{
    public class CosmosDbContext : DbContext
    {
        public CosmosDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Test> Tests { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Data");
            modelBuilder.Entity<Test>().Property(p => p.Id).HasValueGenerator<GuidValueGenerator>();
        }
    }
}