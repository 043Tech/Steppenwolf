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
        private readonly IRepository<BlogPostEntity> blogPostRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public BlogPostController(
            IRepository<BlogPostEntity> blogPostRepository, 
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public Task<BlogPost> GetById(Guid id)
        {
            var blog = this.blogPostRepository.Query()
                .Include(r => r.Author)
                .Include(r => r.BlogCategoryEntities)
                .ThenInclude(r => r.Category)
                .FirstOrDefault(b => b.Id == id);
            return Task.FromResult(this.mapper.Map<BlogPost>(blog));
        }
        
        // Todo take user id from bearer when migrated to web assembly
        public async Task<Guid> Upsert(BlogPost blogPost, string userId)
        {
            Guid postId;
            
            var blog = this.mapper.Map<BlogPostEntity>(blogPost);
            var existingPost = blog.Id != default 
                ? await this.blogPostRepository
                    .Query(false)
                    .Include(b => b.BlogCategoryEntities)
                    .ThenInclude(b => b.Category)
                    .FirstOrDefaultAsync(b => b.Id == blog.Id)
                : null;
            if (existingPost != null)
            {
                postId = await this.blogPostRepository.UpdateAsync(blog);
                await this.categoryRepository.UpdateCategoriesAsync(
                    existingPost,
                    this.mapper.Map<IEnumerable<CategoryEntity>>(blogPost.Categories));
            }
            else
            {
                blog.AuthorId = userId;
                postId = await this.blogPostRepository.AddAsync(blog);
                await this.categoryRepository.UpdateCategoriesAsync(
                    blog,
                    this.mapper.Map<IEnumerable<CategoryEntity>>(blogPost.Categories));
            }
            
            return postId;
        }

        public Task<IEnumerable<BlogPost>> GetAll(BlogPostRequest blogPostRequest)
        {
            var blogs = this.blogPostRepository.Query()
                .Include(r => r.Author)
                .OrderByDescending(b => b.CreatedOn)
                .ThenBy(b => b.Title)
                .Skip(blogPostRequest.Skip + (blogPostRequest.PageIndex * blogPostRequest.PageSize))
                .Take(blogPostRequest.PageSize);
            
            return Task.FromResult(this.mapper.Map<IEnumerable<BlogPost>>(blogs));
        }

        public async Task<int> GetAllCount()
        {
            return await this.blogPostRepository.Query().CountAsync();
        }
    }
}