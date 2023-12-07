using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/touristXP")]
    public class TouristXPController : BaseApiController
    {
        private readonly ITouristXPService _touristXPService;
        public TouristXPController(ITouristXPService touristXPService)
        {
            _touristXPService = touristXPService;
        }


        [HttpGet("{touristId:int}")]
        public ActionResult<PagedResult<TouristXPDto>> GetByUserID(int touristId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _touristXPService.GetByUserId(touristId, page, pageSize);
            return CreateResponse(result);
        }
    }
}
