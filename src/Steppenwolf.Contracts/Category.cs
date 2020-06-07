using System;
using System.Collections.Generic;

namespace Steppenwolf.Contracts
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }
        
        public IEnumerable<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    }
}