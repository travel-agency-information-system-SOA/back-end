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

        public MarketplaceController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourDTO>> GetAll()
        {
            var result = _tourService.GetPublished();
            return CreateResponse(result);
        }
    }
}
