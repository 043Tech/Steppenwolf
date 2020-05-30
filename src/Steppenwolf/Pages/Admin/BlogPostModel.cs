using System.ComponentModel.DataAnnotations;

namespace Steppenwolf.Pages.Admin
{
    public class BlogPostModel
    {
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(20000, MinimumLength = 200)]
        public string Body { get; set; }
        
        [Required]
        [StringLength(300, MinimumLength = 150)]
        public string Preview { get; set; }
    }
}