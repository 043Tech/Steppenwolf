using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steppenwolf.Contracts;
using Steppenwolf.Models;
using Steppenwolf.PostgresRepositories.Interfaces;

namespace Steppenwolf.Services.Data
{
    public class BlogPostController
    {
        private readonly IRepository<BlogPostEntity> repository;
        private readonly IMapper mapper;

        public BlogPostController(IRepository<BlogPostEntity> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task<BlogPost> GetById(Guid id)
        {
            var blog = this.repository.Query().Include(r => r.Author).FirstOrDefault(b => b.Id == id);

            // TODO change when home page is not dummy
            if (blog == null)
            {
                blog = this.repository.Query().Include(r => r.Author).First();
            }

            return Task.FromResult(this.mapper.Map<BlogPost>(blog));
        }

        // Todo take author id from bearer when migrated to web assembly
        public async Task<Guid> Create(BlogPost blogPost, string authorId)
        {
            var blog = this.mapper.Map<BlogPostEntity>(blogPost);
            blog.AuthorId = authorId;

            return await this.repository.AddAsync(blog);
        }
    }
}