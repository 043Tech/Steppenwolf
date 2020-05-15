using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Steppenwolf.Contracts;
using Steppenwolf.Services;

namespace Steppenwolf.Pages.Home
{
    public class HomePageBase : ComponentBase
    {
        protected IEnumerable<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        protected IEnumerable<BlogPost> Featured { get; set; } = new List<BlogPost>();

        protected BlogPost MainBlog => this.Featured.FirstOrDefault() ?? new BlogPost();

        protected IEnumerable<BlogPost> SecondaryBlogs => this.Featured.Skip(1).Take(2);

        protected int Total { get; set; } = 0;

        protected int PageSize { get; set; } = 2;

        protected int PageIndex { get; set; } = 0;

        [Inject]
        private BlogPostService BlogPostService { get; set; }
        
        [Inject]
        private BlazorHelper BlazorHelper { get; set; }

        protected override async void OnInitialized()
        {
            this.Total = await this.BlogPostService.GetAllCount();
            this.BlogPosts = await this.BlogPostService.GetAll(this.PageSize, this.PageIndex);
            this.Featured = await this.BlogPostService.GetAll(3, 0);
        }

        protected async Task Load()
        {
            this.BlogPosts = await this.BlogPostService.GetAll(this.PageSize, this.PageIndex);
        }

        protected string TakeOf(string str, int size)
        {
            return string.Join(" ", str.Split(" ").Take(size)) + "...";
        }

        protected bool DisablePrev()
        {
            return this.PageIndex == 0;
        }

        protected bool DisableNext()
        {
            return (this.PageIndex * this.PageSize) + this.PageSize >= this.Total;
        }

        protected async Task Prev()
        {
            this.PageIndex--;
            await this.Load();
            await this.BlazorHelper.ScrollToFragment("posts");
        }

        protected async Task Next()
        {
            this.PageIndex++;
            await this.Load();
            await this.BlazorHelper.ScrollToFragment("posts");
        }
    }
}