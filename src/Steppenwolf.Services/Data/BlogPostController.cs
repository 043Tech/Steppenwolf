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
            var blog = this.repository.Query()
                .Include(r => r.Author)
                .FirstOrDefault(b => b.Id == id);
            return Task.FromResult(this.mapper.Map<BlogPost>(blog));
        }
        
        // Todo take user id from bearer when migrated to web assembly
        public async Task<Guid> Upsert(BlogPost blogPost, string userId)
        {
            var blog = this.mapper.Map<BlogPostEntity>(blogPost);
            var exist = blog.Id != default && this.repository.Query(true).Any(b => b.Id == blog.Id);
            if (exist)
            {
                return await this.repository.UpdateAsync(blog);
            }
            
            blog.AuthorId = userId;
            return await this.repository.AddAsync(blog);
        }

        public Task<IEnumerable<BlogPost>> GetAll(BlogPostRequest blogPostRequest)
        {
            var blogs = this.repository.Query()
                .Include(r => r.Author)
                .OrderByDescending(b => b.CreatedOn)
                .ThenBy(b => b.Title)
                .Skip(blogPostRequest.Skip + (blogPostRequest.PageIndex * blogPostRequest.PageSize))
                .Take(blogPostRequest.PageSize);
            
            return Task.FromResult(this.mapper.Map<IEnumerable<BlogPost>>(blogs));
        }

        public Task<int> GetAllCount()
        {
            return Task.FromResult(this.repository.Query().Count());
        }
    }
}