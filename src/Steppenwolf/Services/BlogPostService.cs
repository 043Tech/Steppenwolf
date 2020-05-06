using System;
using System.Threading.Tasks;
using Steppenwolf.Contracts;
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
    }
}