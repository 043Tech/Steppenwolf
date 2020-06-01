using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Steppenwolf.Pages.Admin.Category;

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

        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public IList<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
    }
}