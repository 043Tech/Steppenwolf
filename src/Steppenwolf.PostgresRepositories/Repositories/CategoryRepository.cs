using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Steppenwolf.Models;
using Steppenwolf.PostgresRepositories.Context;
using Steppenwolf.PostgresRepositories.Interfaces;

namespace Steppenwolf.PostgresRepositories.Repositories
{
    public class CategoryRepository : Repository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(PostgresDbContext context) 
            : base(context)
        {
        }

        public async Task UpdateCategoriesAsync(BlogPostEntity blogPostEntity, IEnumerable<CategoryEntity> blogPostCategories)
        {
            var existingCategories = blogPostEntity.BlogCategoryEntities.Select(c => c.Category);
            
            // Added 
            var addedCategories = blogPostCategories
                .Where(c => existingCategories.All(ic => ic.Id != c.Id));
                
            // Removed
            var removedCategories = existingCategories
                .Where(c => blogPostCategories.All(ic => ic.Id != c.Id));

            if (removedCategories.Any())
            {
                this.Context.BlogCategories.RemoveRange(removedCategories.Select(c => new BlogCategoryEntity
                {
                    BlogPostId = blogPostEntity.Id,
                    CategoryId = c.Id
                }));   
            }
            
            if (addedCategories.Any())
            {
                await this.Context.BlogCategories.AddRangeAsync(addedCategories.Select(c => new BlogCategoryEntity
                {
                    BlogPostId = blogPostEntity.Id,
                    CategoryId = c.Id
                }));   
            }
            
            await this.Context.SaveChangesAsync();
        }
    }
}