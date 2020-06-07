using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steppenwolf.Models;

namespace Steppenwolf.PostgresRepositories.Interfaces
{
    public interface ICategoryRepository : IRepository<CategoryEntity>
    {
        Task UpdateCategoriesAsync(BlogPostEntity blogPostEntity, IEnumerable<CategoryEntity> blogPostCategories);
    }
}