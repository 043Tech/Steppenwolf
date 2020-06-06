using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Steppenwolf.Contracts;
using Steppenwolf.Models;
using Steppenwolf.Services;

namespace Steppenwolf.Pages.Post
{
    public class CategoryViewBase : ComponentBase
    {
        [Inject] 
        public CategoryService CategoryService { get; set; }
        
        [Inject] 
        public BlogPostService BlogPostService { get; set; }

        [Parameter]
        public string Slug { get; set; }
        
        [Parameter]
        public Contracts.Category Category { get; set; } = new Contracts.Category();

        public IEnumerable<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        protected override async Task OnInitializedAsync()
        {
            await this.LoadCategoryData();
        }

        protected override async Task OnParametersSetAsync()
        {
            await this.LoadCategoryData();
        }

        private async Task LoadCategoryData()
        {
            this.Category = await this.CategoryService.GetBySlugAsync(this.Slug); 
            this.BlogPosts = await this.BlogPostService.GetForCategoryAsync(this.Category.Id, 0, 10);
            this.StateHasChanged();
        }
    }
}