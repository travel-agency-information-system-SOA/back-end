using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Marketplace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Marketplace
{
        [Authorize(Policy = "touristPolicy")]
        [Route("api/tokens")]
        public class TourPurchaseTokenController : BaseApiController
        {
            private readonly ITourPurchaseTokenService _tourTokenService;
            public TourPurchaseTokenController(ITourPurchaseTokenService tourTokenService)
            {
                _tourTokenService = tourTokenService;
            }
            [HttpGet]
            public ActionResult<PagedResult<TourPurchaseTokenDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
            {
                var result = _tourTokenService.GetPaged(page, pageSize);
                return CreateResponse(result);
            }

        [HttpGet("purchasedTours/{touristId}")]
        public ActionResult<PagedResult<TourDTO>> GetPurchasedTours(int touristId)
        {
            try
            {
                var purchasedTours = _tourTokenService.GetPurchasedTours(touristId);

                return Ok(purchasedTours);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal server error");
            }
        }
        

            [HttpGet("purchasedTourss/{touristId:int}")]
            public ActionResult<List<TourDTO>> GetPurchasedTourss([FromRoute] int touristId)
            {
                var result = _tourTokenService.GetPurchasedTours(touristId);
                return CreateResponse(result);
            }
    }
    
}
