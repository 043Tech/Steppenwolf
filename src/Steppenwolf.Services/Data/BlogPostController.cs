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
            return Task.FromResult(this.mapper.Map<BlogPost>(blog));
        }

        public async Task<Guid> Create(BlogPost blogPost, string authorId)
        {
            var blog = this.mapper.Map<BlogPostEntity>(blogPost);
            blog.AuthorId = authorId;

            return await this.repository.AddAsync(blog);
        }

        public Task<IEnumerable<BlogPost>> GetAll(BlogPostRequest blogPostRequest)
        {
            var blogs = this.repository.Query()
                .Include(r => r.Author)
                .OrderByDescending(b => b.CreatedOn)
                .ThenBy(b => b.Title)
                .Skip(blogPostRequest.PageIndex * blogPostRequest.PageSize)
                .Take(blogPostRequest.PageSize);
            
            return Task.FromResult(this.mapper.Map<IEnumerable<BlogPost>>(blogs));
        }

        public Task<int> GetAllCount()
        {
            return Task.FromResult(this.repository.Query().Count());
        }
    }
}