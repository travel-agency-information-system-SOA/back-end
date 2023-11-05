using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;

namespace Explorer.Blog.Core.Mappers;

public class BlogProfile : Profile
{ 
    public BlogProfile()
    {
        CreateMap<BlogPostCommentDto, BlogPostComment>().ReverseMap();
        CreateMap<BlogPostDto, BlogPost>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
            .ForMember(dest => dest.ImageIDs, opt => opt.MapFrom(src => src.ImageIDs))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse(typeof(BlogPostStatus), src.Status)));

        CreateMap<BlogPost, BlogPostDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
            .ForMember(dest => dest.ImageIDs, opt => opt.MapFrom(src => src.ImageIDs))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}