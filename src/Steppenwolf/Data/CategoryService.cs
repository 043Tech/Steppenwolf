using System.Collections.Generic;

namespace Steppenwolf.Data
{
    public class CategoryService
    {
        private readonly IEnumerable<Category> categories = new List<Category>
        {
            new Category { Name = ".NET Core", Slug = "dot-net-core" },
            new Category { Name = "DevOps", Slug = "devops" },
            new Category { Name = "Leadership", Slug = "leadership" },
            new Category { Name = "Frontend", Slug = "frontend" }
        };

        public IEnumerable<Category> GetCategories()
        {
            return this.categories;
        }
    }
}