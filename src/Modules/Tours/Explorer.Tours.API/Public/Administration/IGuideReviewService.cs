using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IGuideReviewService
    {
        Result<PagedResult<GuideReviewDto>> GetPaged(int page, int pageSize);
        Result<GuideReviewDto> Create(GuideReviewDto review);
        Result<GuideReviewDto> Update(GuideReviewDto review);
        Result Delete(int id);

        Result<PagedResult<GuideReviewDto>> GetByGuideIdPaged(int page, int pageSize, int id);
    }
}
