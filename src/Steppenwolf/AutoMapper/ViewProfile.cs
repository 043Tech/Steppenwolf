using AutoMapper;
using Steppenwolf.Contracts;
using Steppenwolf.Pages.Admin;

namespace Steppenwolf.AutoMapper
{
    public class ViewProfile : Profile
    {
        public ViewProfile()
        {
            this.CreateMap<BlogPostCreateModel, BlogPost>();
        }
    }
}