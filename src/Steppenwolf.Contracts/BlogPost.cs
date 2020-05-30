using System;

namespace Steppenwolf.Contracts
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public string Body { get; set; }

        public string Preview { get; set; }

        public Author Author { get; set; }
        
        public DateTime Created { get; set; }
    }
}