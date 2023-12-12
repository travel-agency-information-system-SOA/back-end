using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Mappers;

public class EncountersProfile : Profile
{
    public EncountersProfile() 
    {
        CreateMap<EncounterDto, Encounter>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse(typeof(EncounterStatus), src.Status)))
           .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse(typeof(EncounterType), src.Type)))
           .ForMember(dest => dest.XpPoints, opt => opt.MapFrom(src => src.XpPoints))
           .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
           .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude));

        CreateMap<Encounter,EncounterDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
           .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
           .ForMember(dest => dest.XpPoints, opt => opt.MapFrom(src => src.XpPoints))
           .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
           .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude));

        CreateMap<HiddenLocationEncounterDto, HiddenLocationEncounter>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.EncounterId, opt => opt.MapFrom(src => src.EncounterId))
           .ForMember(dest => dest.DistanceTreshold, opt => opt.MapFrom(src => src.DistanceTreshold))
           .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.ImageURL))
           .ForMember(dest => dest.ImageLongitude, opt => opt.MapFrom(src => src.ImageLongitude))
           .ForMember(dest => dest.ImageLatitude, opt => opt.MapFrom(src => src.ImageLatitude));


        CreateMap<HiddenLocationEncounter, HiddenLocationEncounterDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.EncounterId, opt => opt.MapFrom(src => src.EncounterId))
           .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.ImageURL))
           .ForMember(dest => dest.DistanceTreshold, opt => opt.MapFrom(src => src.DistanceTreshold))
           .ForMember(dest => dest.ImageLatitude, opt => opt.MapFrom(src => src.ImageLatitude))
           .ForMember(dest => dest.ImageLongitude, opt => opt.MapFrom(src => src.ImageLongitude));


        CreateMap<EncounterExecutionDto, EncounterExecution>().ReverseMap();

        CreateMap<SocialEncounterDto, SocialEncounter>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.EncounterId, opt => opt.MapFrom(src => src.EncounterId))
        .ForMember(dest => dest.TouristsRequiredForCompletion, opt => opt.MapFrom(src => src.TouristsRequiredForCompletion))
        .ForMember(dest => dest.DistanceTreshold, opt => opt.MapFrom(src => src.DistanceTreshold))
        .ForMember(dest => dest.TouristIDs, opt => opt.MapFrom(src => src.TouristIDs));

        CreateMap<SocialEncounter, SocialEncounterDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.EncounterId, opt => opt.MapFrom(src => src.EncounterId))
           .ForMember(dest => dest.TouristsRequiredForCompletion, opt => opt.MapFrom(src => src.TouristsRequiredForCompletion))
           .ForMember(dest => dest.DistanceTreshold, opt => opt.MapFrom(src => src.DistanceTreshold))
           .ForMember(dest => dest.TouristIDs, opt => opt.MapFrom(src => src.TouristIDs));


        CreateMap<TourKeyPointEncounterDto, TourKeyPointEncounter>().ReverseMap();
    }
}
