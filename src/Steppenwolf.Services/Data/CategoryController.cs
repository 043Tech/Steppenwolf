using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steppenwolf.Contracts;
using Steppenwolf.Models;
using Steppenwolf.PostgresRepositories.Interfaces;

namespace Steppenwolf.Services.Data
{
    public class CategoryController
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Category>> SearchCategoriesAsync(string name)
        {
            var categories = await this.categoryRepository
                .Query(false)
                .Where(c => c.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();

            return this.mapper.Map<IEnumerable<Category>>(categories);
        }

        public async Task<IEnumerable<Category>> GetAllAsync(int page, int size)
        {
            var categories = await this.categoryRepository
                .Query(false)
                .OrderByDescending(c => c.CreatedOn)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            
            return this.mapper.Map<IEnumerable<Category>>(categories);
        }

        public async Task UpsertAsync(Category category)
        {
            var entity = this.mapper.Map<CategoryEntity>(category);

            if (category.Id == Guid.Empty)
            {
                await this.categoryRepository.AddAsync(entity);
            }

            await this.categoryRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await this.categoryRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return;
            }
            
            await this.categoryRepository.DeleteAsync(entity);
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var entity = await this.categoryRepository
                .Query(false)
                .FirstOrDefaultAsync(e => e.Id == id);
            
            return this.mapper.Map<Category>(entity);
        }

        public async Task<IEnumerable<Category>> GetHeaderCategoriesAsync(int size)
        {
            var categories = await this.categoryRepository
                .Query(false)
                .Include(c => c.BlogCategories)
                .OrderBy(c => c.BlogCategories.Count)
                .ThenByDescending(c => c.CreatedOn)
                .Take(size)
                .ToListAsync();

            return this.mapper.Map<IEnumerable<Category>>(categories);
        }

        public async Task<Category> GetBySlugAsync(string slug)
        {
            var categoryEntity = await this.categoryRepository
                .Query(false)
                .Include(c => c.BlogCategories)
                .ThenInclude(c => c.BlogPost)
                .FirstOrDefaultAsync(c => c.Slug == slug.Trim().ToLower());
 
            return this.mapper.Map<Category>(categoryEntity);
        }
    }
}