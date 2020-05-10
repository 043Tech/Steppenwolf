using System.ComponentModel.DataAnnotations;

namespace Steppenwolf.Pages.Admin
{
    public class BlogPostCreateModel
    {
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(20000, MinimumLength = 200)]
        public string Body { get; set; }
    }
}