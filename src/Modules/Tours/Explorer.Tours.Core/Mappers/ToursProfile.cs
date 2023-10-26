using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();



        CreateMap<TouristEquipmentDto, TouristEquipment>().ReverseMap();

        CreateMap<ProblemDto, Problem>().ReverseMap();



        CreateMap<TourReviewDto, TourReview>().ReverseMap();

        //CreateMap<TourObjectDto, TourObject>().ReverseMap();

         CreateMap<TourObjectDto, TourObject>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
        .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Enum.Parse(typeof(ObjectCategory), src.Category)))
        .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
        .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude));

        CreateMap<TourObject, TourObjectDto>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
        .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
        .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
        .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude));

        CreateMap<ObjInTourDto, ObjInTour>().ReverseMap();
        CreateMap<ObjInTour, ObjInTourDto>().ReverseMap();

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