using System;

namespace Steppenwolf.Models
{
    public class BlogCategoryEntity
    {
        public Guid BlogPostId { get; set; }

        public BlogPostEntity BlogPost { get; set; }

        public Guid CategoryId { get; set; }

        public CategoryEntity Category { get; set; }
    }
}