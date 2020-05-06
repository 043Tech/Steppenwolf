using System;
using Microsoft.AspNetCore.Components;
using Steppenwolf.Contracts;
using Steppenwolf.Services;

namespace Steppenwolf.Pages.PostView
{
    public class PostBase : ComponentBase
    {
        [Inject]
        public BlogPostService BlogPostService { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        protected BlogPost BlogPost { get; set; } = new BlogPost() { Author = new Contracts.Author() };

        protected override async void OnInitialized()
        {
            this.BlogPost = await this.BlogPostService.GetById(this.Id);
        }
    }
}