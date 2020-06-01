using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Steppenwolf.Services;

namespace Steppenwolf.Pages.Post
{
    public class CategoryViewBase : ComponentBase
    {
        [Inject] 
        public CategoryService CategoryService { get; set; }
        
        [Parameter]
        public string Slug { get; set; }
        
        [Parameter]
        public Contracts.Category Category { get; set; } = new Contracts.Category();

        protected override async void OnInitialized()
        {
            var category = await this.CategoryService.GetBySlugAsync(this.Slug);
            
            base.OnInitialized();
        }
    }
}