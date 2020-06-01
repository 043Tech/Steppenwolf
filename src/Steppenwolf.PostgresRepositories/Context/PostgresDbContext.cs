using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Steppenwolf.Models;

namespace Steppenwolf.PostgresRepositories.Context
{
    public class PostgresDbContext : IdentityDbContext<ApplicationUser>
    {
        public PostgresDbContext(DbContextOptions options)
            : base(options)
        {
        }
        
        public DbSet<BlogPostEntity> Blogs { get; set; }

        public DbSet<BlogCategoryEntity> BlogCategories { get; set; }

        public DbSet<CategoryEntity> Categories { get; set; }

        public override int SaveChanges()
        {
            this.OnBeforeSaving();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.OnBeforeSaving();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogCategoryEntity>()
                .HasKey(e => new { e.BlogPostId, e.CategoryId });

            modelBuilder.Entity<BlogCategoryEntity>()
                .HasOne(e => e.BlogPost)
                .WithMany(e => e.BlogCategoryEntities)
                .HasForeignKey(e => e.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<BlogCategoryEntity>()
                .HasOne(e => e.Category)
                .WithMany(e => e.BlogCategories)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            
            base.OnModelCreating(modelBuilder);
        }

        private void OnBeforeSaving()
        {
            var entries = this.ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;
            foreach (var entry in entries)
            {
                if (!(entry.Entity is Entity trackable))
                {
                    continue;
                }

                if (entry.State == EntityState.Modified)
                {
                    trackable.UpdatedOn = utcNow;
                    entry.Property(nameof(Entity.CreatedOn)).IsModified = false;
                }
                
                if (entry.State == EntityState.Added)
                {
                    trackable.CreatedOn = utcNow;
                    trackable.UpdatedOn = utcNow;
                }
                
                if (entry.State == EntityState.Deleted)
                {
                    trackable.IsDeleted = true;
                    trackable.UpdatedOn = utcNow;
                    entry.Property(nameof(Entity.CreatedOn)).IsModified = false;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}