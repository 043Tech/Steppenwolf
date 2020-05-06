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
            // TODO change when we have way to add posts
            var blog = this.repository.Query().Include(r => r.Author).First();
            return Task.FromResult(this.mapper.Map<BlogPost>(blog));
        }
    }
}