using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System.Linq;

namespace Explorer.Stakeholders.Core.UseCases;

public class AppRatingService : CrudService<AppRatingDto, AppRating>, IAppRatingService
{
    public AppRatingService(ICrudRepository<AppRating> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public bool HasUserRated(int userId)
    {
       
        Result<PagedResult<AppRatingDto>> allRatingsResult = GetPaged(1, int.MaxValue);   // Geting all app ratings

        
        if (allRatingsResult.IsSuccess)     // Checking if there is any rating by the specified user
        {
            var allRatings = allRatingsResult.Value.Results;
            return allRatings.Any(rating => rating.UserId == userId);
        }

        return false;
    }
}