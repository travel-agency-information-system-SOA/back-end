using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        CreateMap<GuideReviewDto, GuideReview>().ReverseMap();

        CreateMap<PreferencesDto, Preferences>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.PreferredDifficulty, opt => opt.MapFrom(src => Enum.Parse(typeof(DifficultyLevel), src.PreferredDifficulty)))
            .ForMember(dest => dest.TransportationPreferences, opt => opt.MapFrom(src => src.TransportationPreferences))
            .ForMember(dest => dest.InterestTags, opt => opt.MapFrom(src => src.InterestTags));
        
        CreateMap<Preferences, PreferencesDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.PreferredDifficulty, opt => opt.MapFrom(src => src.PreferredDifficulty.ToString()))
            .ForMember(dest => dest.TransportationPreferences, opt => opt.MapFrom(src => src.TransportationPreferences))
            .ForMember(dest => dest.InterestTags, opt => opt.MapFrom(src => src.InterestTags));
    }
}