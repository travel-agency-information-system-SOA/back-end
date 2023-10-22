using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Review
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/review/guideReview")]
    public class GuideReviewController : BaseApiController
    {
        private readonly IGuideReviewService _guideReviewService;

        public GuideReviewController(IGuideReviewService guideReviewService) 
        {
            _guideReviewService = guideReviewService;
        }

        [HttpGet]
        public ActionResult<PagedResult<GuideReviewDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _guideReviewService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PagedResult<GuideReviewDto>> GetAllByGuideId([FromQuery] int page, [FromQuery] int pageSize, int id)
        {
            var result = _guideReviewService.GetByGuideIdPaged(page, pageSize, id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<GuideReviewDto> Create([FromBody] GuideReviewDto review)
        {
            var result = _guideReviewService.Create(review);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<GuideReviewDto> Update([FromBody] GuideReviewDto review)
        {
            var result = _guideReviewService.Update(review);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _guideReviewService.Delete(id);
            return CreateResponse(result);
        }
    }
}
