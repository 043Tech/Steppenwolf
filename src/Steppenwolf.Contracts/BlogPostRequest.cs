namespace Steppenwolf.Contracts
{
    public class BlogPostRequest
    {
        public int PageSize { get; set; } = 5;

        public int PageIndex { get; set; } = 0;

        public int Skip { get; set; } = 0;
    }
}