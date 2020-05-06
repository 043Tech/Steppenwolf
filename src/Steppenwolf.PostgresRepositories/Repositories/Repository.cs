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
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly PostgresDbContext context;

        public Repository(PostgresDbContext context)
        {
            this.context = context;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await this.context.Set<T>()
                .Where(e => e.IsDeleted == false)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }
        
        public IQueryable<T> Query()
        {
            return this.context.Set<T>().Where(e => e.IsDeleted == false);
        }

        public async Task AddAsync(T entity)
        {
            await this.context.AddAsync(entity);
            await this.SaveAsync();
        }
        
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await this.context.AddAsync(entities);
            await this.SaveAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            this.context.Update(entity);
            await this.SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            this.context.Remove(entity);
            await this.SaveAsync();
        }

        private async Task SaveAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}