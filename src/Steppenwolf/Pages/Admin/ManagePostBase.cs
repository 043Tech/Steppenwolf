using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Steppenwolf.Contracts;
using Steppenwolf.Pages.Admin.Category;
using Steppenwolf.Services;
using Steppenwolf.Shared;

namespace Steppenwolf.Pages.Admin
{
    public class ManagePostBase : ComponentBase
    {
        [Parameter]
        public Guid Id { get; set; }

        protected BlogPostModel BlogPostModel { get; set; } = new BlogPostModel();

        protected BlogPost EditBlogPost { get; set; }

        protected bool Preview { get; set; } = false;

        [Inject]
        private HeadState HeadState { get; set; }

        [Inject]
        private BlogPostService BlogPostService { get; set; }

        [Inject]
        private CategoryService CategoryService { get; set; }

        [Inject]
        private IMapper Mapper { get; set; }

        [Inject]
        private IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

        [Inject]
        private NavigationManager Navigation { get; set; }

        protected async Task<IEnumerable<CategoryModel>> SearchCategories(string searchText)
        {
            var result = await this.CategoryService.SearchCategoriesAsync(searchText);
            result = result
                .Where(c1 => this.BlogPostModel.Categories.All(c2 => c2.Id != c1.Id))
                .ToList();

            var categories = this.Mapper.Map<IList<CategoryModel>>(result);

            return categories;
        }

        protected override async void OnInitialized()
        {
            this.HeadState.AddStyleItem("_content/LoreSoft.Blazor.Controls/BlazorControls.css");

            var blog = new BlogPost()
            {
                Author = new Author()
                {
                    Name = this.HttpContextAccessor.HttpContext.User.Identity.Name
                }
            };
            
            if (this.Id != default)
            {
                var apiBlog = await this.BlogPostService.GetById(this.Id);
                if (apiBlog != null)
                {
                    blog = apiBlog;
                }
            }

            this.EditBlogPost = blog;
            this.BlogPostModel = this.Mapper.Map<BlogPostModel>(this.EditBlogPost);
        }

        protected async Task Submit()
        {
            var blog = this.Mapper.Map(this.BlogPostModel, this.EditBlogPost);
            var authorId = this.HttpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
            var id = await this.BlogPostService.Upsert(blog, authorId);
            this.Navigation.NavigateTo($"/post/{id.ToString()}");
        }

        protected BlogPost GetBlogPostPreviewModel()
        {
            var blog = this.Mapper.Map(this.BlogPostModel, this.EditBlogPost);
            return blog;
        }
    }
}