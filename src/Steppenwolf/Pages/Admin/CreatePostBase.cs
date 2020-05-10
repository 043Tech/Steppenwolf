using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Steppenwolf.Contracts;
using Steppenwolf.Services;

namespace Steppenwolf.Pages.Admin
{
    public class CreatePostBase : ComponentBase
    {
        protected BlogPostCreateModel BlogPostCreateModel { get; set; } = new BlogPostCreateModel();

        protected bool Preview { get; set; } = false;
        
        [Inject]
        private BlogPostService Service { get; set; }

        [Inject]
        private IMapper Mapper { get; set; }
        
        [Inject]
        private IHttpContextAccessor HttpContextAccessor { get; set; } = default!;
        
        [Inject]
        private NavigationManager Navigation { get; set; }

        protected async Task Create()
        {
            var blog = this.Mapper.Map<BlogPost>(this.BlogPostCreateModel);
            var authorId = this.HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
            var id = await this.Service.Create(blog, authorId);
            this.Navigation.NavigateTo($"/post/{id.ToString()}");
        }

        protected BlogPost GetBlogPostPreviewModel()
        {
            var blog = this.Mapper.Map<BlogPost>(this.BlogPostCreateModel);
            blog.Author = new Author()
            {
                Name = this.HttpContextAccessor.HttpContext.User.Identity.Name
            };

            return blog;
        }
    }
}