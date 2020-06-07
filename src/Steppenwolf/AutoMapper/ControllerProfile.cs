using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Steppenwolf.Contracts;
using Steppenwolf.Models;

namespace Steppenwolf.AutoMapper
{
    public class ControllerProfile : Profile
    {
        public ControllerProfile()
        {
            this.CreateMap<BlogPostEntity, BlogPost>()
                .ForMember(
                    dest => dest.Created,
                    opt => opt.MapFrom(src => src.CreatedOn)
                )
                .ForMember(
                    dest => dest.Categories,
                    opt => opt.MapFrom<IEnumerable<Category>>(
                        (s, d, e, c) => c.Mapper.Map<IEnumerable<Category>>(s.BlogCategoryEntities.Select(t => t.Category))));
            
            this.CreateMap<ApplicationUser, Author>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.UserName)
                );

            this.CreateMap<BlogPost, BlogPostEntity>()
                .ForMember(
                    dest => dest.AuthorId,
                    opt => opt.MapFrom(src => src.Author.Id)
                )
                .ForMember(
                    dest => dest.Author,
                    opt => opt.Ignore()
                );

            this.CreateMap<CategoryEntity, Category>();
            this.CreateMap<Category, CategoryEntity>();
        }
    }
}