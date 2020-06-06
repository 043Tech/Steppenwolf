using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steppenwolf.Contracts;
using Steppenwolf.Models;
using Steppenwolf.Services.Data;

namespace Steppenwolf.Services
{
    public class BlogPostService
    {
        private readonly BlogPostController controller;

        // TODO change to HTTP for blazor web assembly
        public BlogPostService(BlogPostController controller)
        {
            this.controller = controller;
        }

        public async Task<BlogPost> GetById(Guid id)
        {
            return await this.controller.GetById(id);
        }

        public async Task<Guid> Upsert(BlogPost blogPost, string userId)
        {
            return await this.controller.Upsert(blogPost, userId);
        }

        public async Task<IEnumerable<BlogPost>> GetAll(int pageSize, int pageIndex, int skip = 0)
        {
            var blogPostRequest = new BlogPostRequest
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Skip = skip
            };
                
            return await this.controller.GetAll(blogPostRequest);
        }

        public async Task<int> GetAllCount()
        {
            return await this.controller.GetAllCount();
        }

        public async Task<IEnumerable<BlogPost>> GetForCategoryAsync(Guid categoryId, int pageIndex, int pageSize)
        {
            return await this.controller.GetForCategoryAsync(categoryId, pageIndex, pageSize);
        }
    }
}