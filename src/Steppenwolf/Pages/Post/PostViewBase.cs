using Microsoft.AspNetCore.Components;
using Steppenwolf.Contracts;

namespace Steppenwolf.Pages.Post
{
    public class PostViewBase : ComponentBase
    {
        [Parameter]
        public BlogPost BlogPost { get; set; }
    }
}