using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Steppenwolf.Models;

namespace Steppenwolf.PostgresRepositories.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        public Task<T> GetByIdAsync(Guid id);
        
        public IQueryable<T> Query(bool tracking = false);
        
        public Task<Guid> AddAsync(T entity);
        
        public Task AddRangeAsync(IEnumerable<T> entities);

        public Task<Guid> UpdateAsync(T entity);
        
        public Task UpdateRangeAsync(IEnumerable<T> entities);

        public Task DeleteAsync(T entity);
    }
}