using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Steppenwolf.CosmosRepositories.Context;
using Steppenwolf.CosmosRepositories.Interfaces;
using Steppenwolf.Models;

namespace Steppenwolf.CosmosRepositories.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly CosmosDbContext context;

        public Repository(CosmosDbContext context)
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