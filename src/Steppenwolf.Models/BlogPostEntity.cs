using System.Collections.Generic;

namespace Steppenwolf.Models
{
    public class BlogPostEntity : Entity
    {
        public string AuthorId { get; set; }
        
        public ApplicationUser Author { get; set; }
        
        public string Title { get; set; }

        public string Body { get; set; }

        public string Preview { get; set; }
        
        public ICollection<BlogCategoryEntity> BlogCategoryEntities { get; set; }
    }
}