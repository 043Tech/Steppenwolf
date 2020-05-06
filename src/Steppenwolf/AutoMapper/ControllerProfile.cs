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
                );
            
            this.CreateMap<ApplicationUser, Author>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.UserName)
                );
        }
    }
}