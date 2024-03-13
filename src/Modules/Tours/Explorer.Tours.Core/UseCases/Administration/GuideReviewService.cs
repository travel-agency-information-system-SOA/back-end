using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class GuideReviewService : CrudService<GuideReviewDto,GuideReview>, IGuideReviewService
    {
        public GuideReviewService(ICrudRepository<GuideReview> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<PagedResult<GuideReviewDto>> GetByGuideIdPaged(int page, int pageSize,int id)
        {
            var allGuideReviews = GetPaged(page, pageSize).Value;
            var filteredGuideReviews = allGuideReviews.Results.Where(review => review.GuideId == id).ToList();
            var pagedResult = new PagedResult<GuideReviewDto>(filteredGuideReviews, filteredGuideReviews.Count);

            return Result.Ok(pagedResult);
        }
    }
}
