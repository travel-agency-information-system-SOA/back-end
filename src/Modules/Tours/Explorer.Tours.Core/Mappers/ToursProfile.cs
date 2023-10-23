using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();

		CreateMap<TourDTO, Tour>()
	 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
	 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
	  .ForMember(dest => dest.DifficultyLevel, opt => opt.MapFrom(src => Enum.Parse(typeof(DifficultyLevel), src.DifficultyLevel)))
      .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse(typeof(TourStatus), src.Status)))
	 .ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId))
	 .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
	 .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));

		CreateMap<Tour, TourDTO>()
		.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
		.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
		.ForMember(dest => dest.DifficultyLevel, opt => opt.MapFrom(src => src.DifficultyLevel.ToString()))
		.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
		.ForMember(dest => dest.GuideId, opt => opt.MapFrom(src => src.GuideId))
	    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
		.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));

        CreateMap<TourEquipmentDto, TourEquipment>().ReverseMap();


		
	

        CreateMap<TourPointDto, TourPoint>().ReverseMap();
    }
}