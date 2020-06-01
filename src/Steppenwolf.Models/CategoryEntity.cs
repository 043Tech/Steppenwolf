using System.Collections.Generic;

namespace Steppenwolf.Models
{
    public class CategoryEntity : Entity
    {
        public string Name { get; set; }

        public string Slug { get; set; }

        public ICollection<BlogCategoryEntity> BlogCategories { get; set; } = new List<BlogCategoryEntity>();
    }
}