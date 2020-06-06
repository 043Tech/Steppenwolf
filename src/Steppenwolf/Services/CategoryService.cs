using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steppenwolf.Contracts;
using Steppenwolf.Services.Data;

namespace Steppenwolf.Services
{
    public class CategoryService
    {
        private readonly CategoryController controller;

        // TODO change to HTTP for blazor web assembly
        public CategoryService(CategoryController controller)
        {
            this.controller = controller;
        }

        public async Task<IEnumerable<Category>> SearchCategoriesAsync(string name)
        {
            return await this.controller.SearchCategoriesAsync(name);
        }

        public async Task<IEnumerable<Category>> GetAllAsync(int page, int size)
        {
            return await this.controller.GetAllAsync(page, size);
        }

        public async Task UpsertAsync(Category category)
        {
            await this.controller.UpsertAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            await this.controller.DeleteAsync(id);
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await this.controller.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Category>> GetHeaderCategoriesAsync(int size)
        {
            return await this.controller.GetHeaderCategoriesAsync(size);
        }

        public async Task<Category> GetBySlugAsync(string slug)
        {
            return await this.controller.GetBySlugAsync(slug);
        }
    }
}