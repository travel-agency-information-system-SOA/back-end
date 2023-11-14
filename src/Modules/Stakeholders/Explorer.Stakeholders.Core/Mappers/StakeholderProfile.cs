using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Problems;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<AppRatingDto, AppRating>().ReverseMap();
        
        CreateMap<UserProfileDto, Person>().ReverseMap();

        CreateMap<ClubDto, Club>().ReverseMap();
        CreateMap<ProblemDto, Problem>().ReverseMap();
        CreateMap<ProblemMessageDto, ProblemMessage>().ReverseMap();
    }
}