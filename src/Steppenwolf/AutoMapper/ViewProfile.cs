using System.Linq;
using AutoMapper;
using Steppenwolf.Contracts;
using Steppenwolf.Pages.Admin;
using Steppenwolf.Pages.Admin.Category;

namespace Steppenwolf.AutoMapper
{
    public class ViewProfile : Profile
    {
        public ViewProfile()
        {
            this.CreateMap<BlogPostModel, BlogPost>();
            this.CreateMap<BlogPost, BlogPostModel>();
            this.CreateMap<Category, CategoryModel>();
            this.CreateMap<CategoryModel, Category>();
        }
    }
}