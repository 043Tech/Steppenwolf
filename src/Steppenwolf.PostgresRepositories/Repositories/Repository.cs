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
        public Repository(PostgresDbContext context)
        {
            this.Context = context;
        }
        
        protected PostgresDbContext Context { get; }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await this.Context.Set<T>()
                .Where(e => e.IsDeleted == false)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }
        
        public IQueryable<T> Query(bool tracking = false)
        {
            var query = this.Context.Set<T>().Where(e => e.IsDeleted == false);
            if (tracking)
            {
                return query;
            }

            return query.AsNoTracking();
        }

        public async Task<Guid> AddAsync(T entity)
        {
            await this.Context.AddAsync(entity);
            await this.SaveAsync();

            return entity.Id;
        }
        
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await this.Context.AddAsync(entities);
            await this.SaveAsync();
        }

        public async Task<Guid> UpdateAsync(T entity)
        {
            this.Context.Update(entity);
            await this.SaveAsync();

            return entity.Id;
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            this.Context.UpdateRange(entities);
            await this.SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            this.Context.Remove(entity);
            await this.SaveAsync();
        }

        private async Task SaveAsync()
        {
            await this.Context.SaveChangesAsync();
        }
    }
}