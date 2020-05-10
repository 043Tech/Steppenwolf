﻿using System.Collections.Generic;
using Steppenwolf.Services.Models;

namespace Steppenwolf.Services.Data
{
    public class CategoryController
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