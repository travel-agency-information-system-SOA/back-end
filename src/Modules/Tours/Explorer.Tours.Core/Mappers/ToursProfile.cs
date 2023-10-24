using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        //CreateMap<TourObjectDto, TourObject>().ReverseMap();

         CreateMap<TourObjectDto, TourObject>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
        .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Enum.Parse(typeof(ObjectCategory), src.Category)));

        CreateMap<TourObject, TourObjectDto>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
        .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()));
 
    }
}