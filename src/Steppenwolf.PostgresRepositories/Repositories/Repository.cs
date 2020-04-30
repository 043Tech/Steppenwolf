using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IQueryable<T> Query()
        {
            return this.context.Set<T>();
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

        private async Task SaveAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}