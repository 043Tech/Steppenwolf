using System;
using Microsoft.AspNetCore.Components;
using Steppenwolf.Contracts;
using Steppenwolf.Services;

namespace Steppenwolf.Pages.Post
{
    public class PostPageBase : ComponentBase
    {
        [Parameter]
        public Guid Id { get; set; }

        protected BlogPost BlogPost { get; set; } = new BlogPost() { Author = new Contracts.Author() };
        
        [Inject]
        private BlogPostService BlogPostService { get; set; }

        protected override async void OnInitialized()
        {
            this.BlogPost = await this.BlogPostService.GetById(this.Id);
        }
    }
}