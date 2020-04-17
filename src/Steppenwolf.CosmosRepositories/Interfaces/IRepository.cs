using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Steppenwolf.Models;

namespace Steppenwolf.CosmosRepositories.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        public IQueryable<T> Query();
        
        public Task AddAsync(T entity);
        
        public Task AddRangeAsync(IEnumerable<T> entity);

        public Task SaveAsync();
    }
}