using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Steppenwolf.Contracts;
using Steppenwolf.Services;

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
        private BlogPostService Service { get; set; }

        [Inject]
        private IMapper Mapper { get; set; }

        [Inject]
        private IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

        [Inject]
        private NavigationManager Navigation { get; set; }

        protected override async void OnInitialized()
        {
            var blog = new BlogPost()
            {
                Author = new Author()
                {
                    Name = this.HttpContextAccessor.HttpContext.User.Identity.Name
                }
            };
            
            if (this.Id != default)
            {
                var apiBlog = await this.Service.GetById(this.Id);
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
            var id = await this.Service.Upsert(blog, authorId);
            this.Navigation.NavigateTo($"/post/{id.ToString()}");
        }

        protected BlogPost GetBlogPostPreviewModel()
        {
            var blog = this.Mapper.Map(this.BlogPostModel, this.EditBlogPost);
            return blog;
        }
    }
}