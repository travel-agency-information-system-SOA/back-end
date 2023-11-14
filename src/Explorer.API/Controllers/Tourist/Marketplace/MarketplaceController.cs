using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Marketplace
{

    [Authorize(Policy = "touristPolicy")]
    [Route("api/marketplace")]
    public class MarketplaceController : BaseApiController
    {
        private readonly ITourService _tourService;
        private readonly ITourPointService _tourPointService;

        public MarketplaceController(ITourService tourService, ITourPointService tourPointService)
        {
            _tourService = tourService;
            _tourPointService = tourPointService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourDTO>> GetAll()
        {
            var result = _tourService.GetPublished();
            foreach(var tour in result.Value.Results)
            {
                var tourPoints = _tourPointService.GetTourPointsByTourId(tour.Id);
                tour.TourPoints = tourPoints.Value.Results;
            }
            return CreateResponse(result);
        }
    }
}
